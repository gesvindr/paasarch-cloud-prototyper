using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Entities;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.Model.Operations.Communication;
using CloudPrototyper.Model.Operations.DataAccess;
using CloudPrototyper.Model.Resources.Storage;
using System.Collections.Generic;
using CloudPrototyper.Azure.Resources;
using CloudPrototyper.NET.Framework.v462.Computing.Models;
using CloudPrototyper.NET.Framework.v462.TblStorage.Model;

namespace CloudPrototyper.Examples
{
    /// <summary>
    /// Example of the model instatnce with a solution representing a social network.
    /// </summary>
    public class SocialNetworkSample
    {
        /// <summary>
        /// Instance of prototype Model.
        /// </summary>
        public Prototype Model { get; set; }

        /// <summary>
        /// Model definition using instances of Applications, Actions/Operations, Entities and Resources.
        /// </summary>

        public SocialNetworkSample()
        {
            Model = new Prototype()
            {
                Applications = new List<Application>()
    {
        new RestApiApplication()
        {
            Name = "SocialNetworkRestApi",
            Platform = "DotNet46",
            Actions = new List<CallableAction>()
            {
                new CallableAction()
                {
                    Name = "InsertWallRecord",
                    Operation = new SequenceOperation()
                    {
                        Name = "InsertWallRecordSeqOperation",
                        Operations = new List<Operation>()
                        {
                            new AddMessageToQueue()
                            {
                                Name = "InsertWallRecordTaskToWorkerQueue",
                                QueueName = "WorkerQueue",
                                EntityName = "InsertWallRecordTask"
                            }
                        }
                    }
                },
                new CallableAction()
                {
                    Name = "GetWallRecords",
                    Operation = new SequenceOperation()
                    {
                        Name = "GetWallRecordsSeq",
                        Operations = new List<Operation>()
                        {
                            new LoadEntitiesFromEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkStorage",
                                EntitySetName = "WallRecords",
                                EntityName = "WallRecord",
                                Name = "Take30WallRecordsFromSocialNetworkStorage",
                                Filter = new FilterCondition()
                                {
                                    NumberOfResults = 30,
                                    UseKey = true,
                                }
                            }
                            /*,
                            new CallUrlOperation
                            {
                                Name =  "WIkiLondon",
                                Url = "https://en.wikipedia.org/w/api.php?action=query&prop=extracts&exlimit=max&explaintext&format=json&titles=london"
                            },
                            new ImageTresholding
                            {
                                Name   = "LenaThresholding"
                            }*/
                        }
                       
                    }
                },
                new CallableAction()
                {
                    Name = "GetWallRecordsDB",
                    Operation = new SequenceOperation()
                    {
                        Name = "GetWallRecordsDBSeq",
                        Operations = new List<Operation>()
                        {
                            new LoadEntitiesFromEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkDB",
                                EntitySetName = "WallRecords",
                                EntityName = "WallRecord",
                                Name = "Take30WallRecordsFromDB",
                                Filter = new FilterCondition()
                                {
                                    NumberOfResults = 30,
                                    UseKey = true,
                                }
                            },
                            new LoadEntitiesFromEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkDB",
                                EntitySetName = "WallRecords",
                                EntityName = "WallRecord",
                                Name = "Take30WallRecordsFromDB2",
                                Filter = new FilterCondition()
                                {
                                    NumberOfResults = 30,
                                    UseKey = true,
                                }
                            },
                            new LoadEntitiesFromEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkDB",
                                EntitySetName = "WallRecords",
                                EntityName = "WallRecord",
                                Name = "Take30WallRecordsFromDB3",
                                Filter = new FilterCondition()
                                {
                                    NumberOfResults = 30,
                                    UseKey = true,
                                }
                            }
                        }
                    }
                }
            }
        },
        new WorkerApplication()
        {
            Name = "SocialNetworkWorkerApp",
            Platform = "DotNet46",
            Actions = new List<TriggeredAction>()
            {
                new TriggeredAction()
                {
                    Name = "InsertWallRecord",
                    Trigger = new MessageReceivedTrigger()
                    {
                        MessageType = "InsertWallRecordTask",
                        QueueName = "WorkerQueue"
                    },
                    Operation = new SequenceOperation()
                    {
                        Name = "InsertWallRecordSeq",
                        Operations = new List<Operation>()
                        {
                            new LoadEntitiesFromEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkDB",
                                EntitySetName = "WallRecords",
                                EntityName = "WallRecord",
                                Name = "FindExistingRecordByKeyFromSocialNetworkDB",
                                Filter = new FilterCondition()
                                { // FindByID
                                    UseKey = true,
                                    NumberOfResults = 1
                                }
                            },
                            new LoadEntitiesFromEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkDB",
                                EntitySetName = "User",
                                EntityName = "User",
                                Name = "ValidateUserPermission",
                                Filter = new FilterCondition()
                                { // FindByID
                                    UseKey = true,
                                    NumberOfResults = 1
                                }
                            },
                            new InsertEntityToEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkDB",
                                EntitySetName = "WallRecords",
                                EntityName = "WallRecord",
                                Name = "OneWallRecordToSocialNetworkDB",
                                NumberOfEntities = 1
                            },
                            new AddMessageToQueue()
                            {
                                QueueName = "WorkerQueue",
                                EntityName = "GenerateWallRecordTask",
                                Name = "AddGenerateWallRecordTaskToWorkerQueue"
                            }
                        }
                    }
                },
                new TriggeredAction()
                {
                    Name = "GenerateWallRecordTask",
                    Trigger = new MessageReceivedTrigger()
                    {
                        MessageType = "GenerateWallRecordTask",
                        QueueName = "WorkerQueue"
                    },
                    Operation = new SequenceOperation()
                    {
                        Name = "GenerateWallRecordTaskSeq",
                        Operations = new List<Operation>()
                        {
                            new LoadEntitiesFromEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkDB",
                                EntitySetName = "WallRecords",
                                EntityName = "WallRecord",
                                Name = "FindOneWallRecordByKeyFromSocialNetworkDB",
                                Filter = new FilterCondition()
                                { // FindByID
                                    UseKey = true,
                                    NumberOfResults = 1
                                }
                            },
                            new InsertEntityToEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkStorage",
                                EntitySetName = "WallRecords",
                                EntityName = "WallRecord",
                                NumberOfEntities = 30,
                                Name = "InsertThirtyWallRecordsToSocialNetworkStorage"
                            },
                            new InsertEntityToEntityStorage()
                            {
                                EntityStorageName = "SocialNetworkStorage",
                                EntitySetName = "PrivateMessages",
                                EntityName = "WallRecord",
                                Name = "InsertTwoWallRecordsToSocialNetworkStorage",
                                NumberOfEntities = 2
                            }
                        }
                    }
                }
            }
        }
    },
                Entities = new List<Model.Entities.Entity>()
    {
        new CloudPrototyper.Model.Entities.Entity()
        {
            Name = "WallRecord",
            Properties = new List<PropertyInfo>()
            {
                new PropertyInfo() { Name = "Number1", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number2", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number3", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number4", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number5", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Message", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message2", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message3", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message4", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message5", Type = "System.String", ContentSize = 500 }
            }
        },
        new CloudPrototyper.Model.Entities.Entity()
        {
            Name = "InsertWallRecordTask",
            Properties = new List<PropertyInfo>()
            {
                new PropertyInfo() { Name = "Number1", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number2", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number3", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number4", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number5", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Message", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message2", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message3", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message4", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message5", Type = "System.String", ContentSize = 500 }
            }
        },
        new CloudPrototyper.Model.Entities.Entity()
        {
            Name = "GenerateWallRecordTask",
            Properties = new List<PropertyInfo>()
            {
                new PropertyInfo() { Name = "Number1", Type = "System.Int32" }
            }
        },
        new CloudPrototyper.Model.Entities.Entity()
        {
            Name = "User",
            Properties = new List<PropertyInfo>()
            {
                new PropertyInfo() { Name = "Number1", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number2", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number3", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number4", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Number5", Type = "System.Int32", ContentSize = 8},
                new PropertyInfo() { Name = "Message", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message2", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message3", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message4", Type = "System.String", ContentSize = 50 },
                new PropertyInfo() { Name = "Message5", Type = "System.String", ContentSize = 50 }
            }
        },
    },
                Resources = new List<Model.Resources.Resource>()
    {
        new AzureSQLDatabase()
        {
            Name = "SocialNetworkDB",
            DeployTo = "Azure",
            PerformanceTier = "standard",
            ServiceObjective = "S3",
            EntitySets = new List<EntitySet>()
            {
                new EntitySet() { EntityName = "WallRecord", Name ="WallRecords", Count = 25000 },
                new EntitySet() { EntityName = "User", Name ="Users", Count = 5000 }
            }
        },
        new AzureTableStorage()
        {
            Name = "SocialNetworkStorage",
            DeployTo = "Azure",
            EntitySets = new List<EntitySet>()
            {
                new EntitySet() { Name = "WallRecords", EntityName = "WallRecord", Count = 25000 },
                new EntitySet() { Name = "PrivateMessages", EntityName = "WallRecord", Count = 5000 },
                new EntitySet() { Name = "ImportantWallRecords", EntityName = "WallRecord", Count = 5000 },
                new EntitySet() { Name = "FavouriteWallRecords", EntityName = "WallRecord", Count = 5000 }
            }
        },
        new AzureAppService()
        {
            DeployTo = "Azure",
            Name = "HostingApi",
            PerformanceTier =  "StandardS3",
            WithApplication = "SocialNetworkRestApi"

        },
        new AzureAppService()
        {
            DeployTo = "Azure",
            Name = "HostingWorker",
            PerformanceTier = "StandardS3",
            WithApplication = "SocialNetworkWorkerApp"

        },
        new AzureServiceBusQueue()
        {
            DeployTo = "Azure",
            Name = "WorkerQueue",
            SizeInGB = 1
        }
    }
            };
        }
    }
}
