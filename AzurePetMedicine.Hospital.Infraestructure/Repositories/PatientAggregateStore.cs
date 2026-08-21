using AzurePetMedicine.Hospital.Domain.Entities;
using AzurePetMedicine.Hospital.Domain.Repositories;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using AzurePetMedicine.Hospital.Infraestructure.Events;
using AzurePetMedicine.Common.Events;
using Newtonsoft.Json;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;

namespace AzurePetMedicine.Hospital.Infraestructure.Repositories
{
    public class PatientAggregateStore : IPatientAggregateStore
    {

        private readonly IMongoCollection<PatientEventData> _collection;

        public PatientAggregateStore(IConfiguration configuration) {            
            var client = new MongoClient("mongodb+srv://user:password@cluster0.faf6mcj.mongodb.net/?appName=Cluster0");
            var database = client.GetDatabase("Hospital");
            _collection = database.GetCollection<PatientEventData>("Patients");
        }

        public async Task<Patient> LoadAsync(Guid id)
        {
            if(id == Guid.Empty) {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }

            var aggregateId = $"Patient-{id}";
            var allEvents = new List<PatientEventData>();

            allEvents = _collection.Find(c => c.aggregateId == aggregateId).ToList();

            var domainEvents = allEvents.Select(e =>
            {
                var assemblyQualifiedName = JsonConvert.DeserializeObject<string>(e.AssemblyQualifiedName)
                    ?? throw new Exception($"AssemblyQualifiedName cannot be empty for {id}");
                var eventType = Type.GetType(assemblyQualifiedName)
                    ?? throw new Exception($"eventType cannot be empty for {id}"); ;
                var data = JsonConvert.DeserializeObject(e.data,eventType);
                return data as IDomainEvent;
            });

            var aggregate = new Patient();
            aggregate.Load(domainEvents);
            return aggregate;
        }

        public async Task SaveAsync(Patient patient)
        {
            if(patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            var changes = patient.GetChanges().Select(e=>new PatientEventData(
                Guid.NewGuid(),
                $"Patient-{patient.Id}",
                e.GetType().Name,
                JsonConvert.SerializeObject(e),
                JsonConvert.SerializeObject(e.GetType().AssemblyQualifiedName)
            ));

            if (!changes.Any()) 
            {
                return;
            }

            foreach (var change in changes)
            {
                await _collection.InsertOneAsync(change);
            }
            patient.ClearChanges();            
        }
    }
}
