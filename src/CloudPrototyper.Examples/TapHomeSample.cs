using System.Collections.Generic;
using CloudPrototyper.Azure.Resources;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Entities;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.Model.Operations.Communication;
using CloudPrototyper.Model.Operations.Computing;
using CloudPrototyper.Model.Operations.DataAccess;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.Model.Resources.Storage;
using CloudPrototyper.NET.Framework.v462.Computing.Models;
using CloudPrototyper.NET.Framework.v462.TblStorage.Model;

namespace CloudPrototyper.Examples
{
    public class TapHomeSample
    {
        public Prototype NoSqlVersionAsynchronousModel { get; }
        public Prototype NoSqlVersionSynchronousModel { get; }
        public Prototype SqlVersionSynchronousModel { get; }

        public TapHomeSample()
        {
            NoSqlVersionAsynchronousModel = MakeNoSqlVersionAsynchronousModel();
            NoSqlVersionSynchronousModel = MakeNoSqlVersionSynchronousModel();
            SqlVersionSynchronousModel = MakeSqlVersionSynchronousModel();
        }

        private Prototype MakeNoSqlVersionAsynchronousModel()
        {
            return new Prototype()
            {
                Applications = new List<Application>()
                {
                    new RestApiApplication
                    {
                        Name = "DataCollectingApi",
                        DeployTo = "Azure",
                        Platform = "DotNet46",
                        Actions = new List<CallableAction>
                        {
                            new CallableAction
                            {
                                Name = "SensorDataReportToSave",
                                Operation = new SequenceOperation
                                {
                                    Name = "SensorDataReportToSaveSeq",
                                    Operations = new List<Operation>{

                                        new InsertEntityToEntityStorage()
                                        {
                                            EntityName = "SerializedContainer",
                                            EntitySetName = "SerializedContainers",
                                            EntityStorageName = "SensorDataStorage",
                                            Name = "InsertOneRawToSensorDataStorage",
                                            NumberOfEntities = 1
                                        },
                                    new AddMessageToQueue
                                    {
                                        EntityName = "SerializedContainer",
                                        Name = "AddSerializedContainerToQueue",
                                        QueueName = "ComputeServiceQueue"
                                    }}
                                }
                            }
                        }
                    },

                    new RestApiApplication
                    {
                        Name = "VisualisationDataApi",
                        DeployTo = "Azure",
                        Platform = "DotNet46",
                        Actions = new List<CallableAction>
                        {
                            new CallableAction
                            {
                                Name = "VisualisationDataQuery",
                                Operation = new LoadEntitiesFromEntityStorage
                                {
                                    EntityName = "ProcessedContainer",
                                    Name = "GetProcessedDataToForVisualization",
                                    EntitySetName = "ProcessedContainers",
                                    EntityStorageName = "SensorDataStorage",
                                    Filter = new FilterCondition
                                    {
                                        IsNominal = true,
                                        NumberOfResults = 1,
                                        UseKey = true
                                    }
                                }
                            }
                        }
                    },

                    new WorkerApplication
                    {
                        Name = "ComputeUnit",
                        Platform = "DotNet46",
                        DeployTo = "Azure",
                        Actions = new List<TriggeredAction>
                        {
                            new TriggeredAction()
                            {
                                Trigger = new MessageReceivedTrigger
                                {
                                    MessageType = "SerializedContainer",
                                    QueueName = "ComputeServiceQueue"
                                },
                                Name = "ComputeBatch",
                                Operation = new SequenceOperation()
                                {
                                    Name = "ComputeBatchSeq",
                                    Operations = new List<Operation> {

                                        new LoadEntitiesFromEntityStorage
                                        {
                                            EntityName = "SerializedContainer",
                                            Name = "GetProcessedDataToForVisualization",
                                            EntitySetName = "SerializedContainers",
                                            EntityStorageName = "SensorDataStorage",
                                            Filter = new FilterCondition
                                            {
                                                IsNominal = true,
                                                NumberOfResults = 1,
                                                UseKey = true
                                            }
                                        },
                                        new InsertEntityToEntityStorage()
                                        {
                                            Description = "Inserts processed item to storage",
                                            EntityName = "ProcessedContainer",
                                            EntitySetName = "ProcessedContainers",
                                            EntityStorageName = "SensorDataStorage",
                                            Name = "InsertOneToSensorDataStorage",
                                            NumberOfEntities = 1


                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                Entities = new List<Entity>()
                {
                    new Entity
                    {
                        Name = "SerializedContainer",
                        Description = "Data from 3 hours",
                        Properties = new List<PropertyInfo>
                        {
                            new PropertyInfo
                            {
                                ContentSize = 11250,
                                Name = "SensorData",
                                Type = "System.String"
                            }, new PropertyInfo
                            {
                                ContentSize = 11250,
                                Name = "SensorData2",
                                Type = "System.String"
                            }, new PropertyInfo
                            {
                                ContentSize = 11250,
                                Name = "SensorData3",
                                Type = "System.String"
                            }, new PropertyInfo
                            {
                                ContentSize = 11250,
                                Name = "SensorData4",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 36,
                                Name = "LocationId",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "DeviceId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "StatisticsFunctionId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "ValueLogTypeId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "PeriodId",
                                Type = "System.Int32"
                            },
                        }

                    },
                    new Entity
                    {
                        Name = "ProcessedContainer",
                        Properties = new List<PropertyInfo>
                        {
                            new PropertyInfo
                            {
                                ContentSize = 2000,
                                Name = "SensorData",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 36,
                                Name = "LocationId",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "DeviceId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "StatisticsFunctionId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "ValueLogTypeId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "PeriodId",
                                Type = "System.Int32"
                            },

                        }
                    }
                },
                Resources = new List<Resource>()
                {
                    new AzureTableStorage
                    {
                        DeployTo = "Azure",
                        Name = "SensorDataStorage",
                        EntitySets = new List<EntitySet>
                        {
                            new EntitySet
                            {
                                Count = 10000,
                                EntityName = "SerializedContainer",
                                Name = "SerializedContainers"
                            },
                            new EntitySet
                            {
                                Count = 100,
                                EntityName = "ProcessedContainer",
                                Name = "ProcessedContainers"
                            }
                        }
                    },
                    new AzureServiceBusQueue
                    {
                        DeployTo = "Azure",
                        Name = "ComputeServiceQueue",
                        SizeInGB = 2
                    },
                    new AzureAppService()
                    {
                        DeployTo = "Azure",
                        Name = "VisualisationApi",
                        PerformanceTier =  "StandardS3",
                        WithApplication = "VisualisationDataApi"

                    },
                    new AzureAppService()
                    {
                        DeployTo = "Azure",
                        Name = "CollectingApi",
                        PerformanceTier = "StandardS3",
                        WithApplication = "DataCollectingApi"

                    },
                    new AzureAppService()
                    {
                        DeployTo = "Azure",
                        Name = "ComputeWorker",
                        PerformanceTier = "StandardS3",
                        WithApplication = "ComputeUnit"

                    },
                }
            };

        }
        private Prototype MakeNoSqlVersionSynchronousModel()
        {
            return new Prototype()
            {
                Applications = new List<Application>()
                {
                    new RestApiApplication
                    {
                        Name = "DataCollectingApiNoSQL",
                        DeployTo = "Azure",
                        Platform = "DotNet46",
                        Actions = new List<CallableAction>
                        {
                            new CallableAction
                            {
                                Name = "ComputeBatchOfBatch",
                                Operation = new SequenceOperation
                                {
                                    Name = "ComputeBatchOfBatchSeqOp",
                                    Operations = new List<Operation>
                                    {
                                        new InsertEntityToEntityStorage()
                                        {
                                            EntityName = "SerializedContainer",
                                            EntitySetName = "SerializedContainers",
                                            EntityStorageName = "SensorDataStorage",
                                            Name = "InsertOneRawToSensorDataStorage",
                                            NumberOfEntities = 1
                                        },
                                         new LoadEntitiesFromEntityStorage
                                        {
                                        EntityName = "SerializedContainer",
                                        Name = "GetProcessedDataToForVisualization",
                                        EntitySetName = "SerializedContainers",
                                        EntityStorageName = "SensorDataStorage",
                                        Filter = new FilterCondition
                                        {
                                            IsNominal = true,
                                            NumberOfResults = 1,
                                            UseKey = true
                                        }
                                    },
                                        new InsertEntityToEntityStorage()
                                        {
                                            Description = "Inserts processed item to storage",
                                            EntityName = "ProcessedContainer",
                                            EntitySetName = "ProcessedContainers",
                                            EntityStorageName = "SensorDataStorage",
                                            Name = "InsertOneToSensorDataStorage",
                                            NumberOfEntities = 1
                                        }

                                    }
                                }
                            }
                        }
                    },

                    new RestApiApplication
                    {
                        Name = "VisualisationDataApiNoSQL",
                        DeployTo = "Azure",
                        Platform = "DotNet46",
                        Actions = new List<CallableAction>
                        {
                            new CallableAction
                            {
                                Name = "VisualisationDataQuery",
                                Operation = new LoadEntitiesFromEntityStorage
                                {
                                    EntityName = "ProcessedContainer",
                                    Name = "GetProcessedDataToForVisualization",
                                    EntitySetName = "ProcessedContainers",
                                    EntityStorageName = "SensorDataStorage",
                                    Filter = new FilterCondition
                                    {
                                        IsNominal = true,
                                        NumberOfResults = 1,
                                        UseKey = true
                                    }
                                }
                            }
                        }
                    }
                },
                Entities = new List<Entity>()
                {
                    new Entity
                    {
                        Name = "SerializedContainer",
                        Description = "Data from 3 hours",
                        Properties = new List<PropertyInfo>
                        {
                            new PropertyInfo
                            {
                                ContentSize = 11250,
                                Name = "SensorData",
                                Type = "System.String"
                            }, new PropertyInfo
                            {
                                ContentSize = 11250,
                                Name = "SensorData2",
                                Type = "System.String"
                            }, new PropertyInfo
                            {
                                ContentSize = 11250,
                                Name = "SensorData3",
                                Type = "System.String"
                            }, new PropertyInfo
                            {
                                ContentSize = 11250,
                                Name = "SensorData4",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 36,
                                Name = "LocationId",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "DeviceId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "StatisticsFunctionId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "ValueLogTypeId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "PeriodId",
                                Type = "System.Int32"
                            },
                        }

                    },
                    new Entity
                    {
                        Name = "ProcessedContainer",
                        Properties = new List<PropertyInfo>
                        {
                            new PropertyInfo
                            {
                                ContentSize = 2000,
                                Name = "SensorData",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 36,
                                Name = "LocationId",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "DeviceId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "StatisticsFunctionId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "ValueLogTypeId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "PeriodId",
                                Type = "System.Int32"
                            },

                        }
                    }
                },
                Resources = new List<Resource>()
                {
                    new AzureTableStorage
                    {
                        DeployTo = "Azure",
                        Name = "SensorDataStorage",
                        EntitySets = new List<EntitySet>
                        {
                            new EntitySet
                            {
                                Count = 10000,
                                EntityName = "SerializedContainer",
                                Name = "SerializedContainers"
                            },
                            new EntitySet
                            {
                                Count = 1000,
                                EntityName = "ProcessedContainer",
                                Name = "ProcessedContainers"
                            }
                        }
                    },
                    new AzureAppService()
                    {
                        DeployTo = "Azure",
                        Name = "VisualisationApi",
                        PerformanceTier =  "StandardS3",
                        WithApplication = "VisualisationDataApiNoSQL"

                    },
                    new AzureAppService()
                    {
                        DeployTo = "Azure",
                        Name = "CollectingApi",
                        PerformanceTier = "StandardS3",
                        WithApplication = "DataCollectingApiNoSQL"

                    }
                }
            };

        }
        private Prototype MakeSqlVersionSynchronousModel()
        {
            return new Prototype()
            {
                Applications = new List<Application>()
                {
                    new RestApiApplication
                    {
                        Name = "DataCollectingApiSQL",
                        DeployTo = "Azure",
                        Platform = "DotNet46",
                        Actions = new List<CallableAction>
                        {
                            new CallableAction
                            {
                                Name = "ComputeBatchOfBatch",
                                Operation = new SequenceOperation
                                {
                                    Name = "ComputeBatchOfBatchSeqOp",
                                    NumberOfRepetitions = 1,
                                    Operations = new List<Operation>
                                    {

                                        new InsertEntityToEntityStorage()
                                        {
                                            Description = "Inserts processed item to storage",
                                            EntityName = "Measurement",
                                            EntitySetName = "Measurements",
                                            EntityStorageName = "SensorDataStorage",
                                            Name = "InsertOneToSensorDataStorage",
                                            NumberOfEntities = 45
                                        }
                                    }
                                }
                            }
                        }
                    },

                    new RestApiApplication
                    {
                        Name = "VisualisationDataApiSQL",
                        DeployTo = "Azure",
                        Platform = "DotNet46",
                        Actions = new List<CallableAction>
                        {
                            new CallableAction
                            {
                                Name = "VisualisationDataQuery",
                                Operation = new SequenceOperation()
                                    {
                                    Name = "VisualisationDataQuerySeqOp",
                                    NumberOfRepetitions = 10,
                                    Operations = new List<Operation>
                                    {

                                        new LoadEntitiesFromEntityStorage
                                        {
                                        EntityName = "Measurement",
                                        Name = "GetProcessedDataForVisualization",
                                        EntitySetName = "Measurements",
                                        EntityStorageName = "SensorDataStorage",
                                        Filter = new FilterCondition
                                        {
                                                NumberOfResults = 30,
                                                UseKey = true
                                        }
                                    }
                                    }
                                }

                            }
                        }
                    }
                },
                Entities = new List<Entity>
                {
                    new Entity
                    {
                        Name = "Measurement",
                        Properties = new List<PropertyInfo>
                        {
                            new PropertyInfo
                            {
                                ContentSize = 50,
                                Name = "SensorData",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 36,
                                Name = "LocationId",
                                Type = "System.String"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "DeviceId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "StatisticsFunctionId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "ValueLogTypeId",
                                Type = "System.Int32"
                            },
                            new PropertyInfo
                            {
                                ContentSize = 9,
                                Name = "PeriodId",
                                Type = "System.Int32"
                            },
                        }

                    }
                },
                Resources = new List<Resource>()
                {
                    new AzureSQLDatabase()
                    {
                        DeployTo = "Azure",
                        Name = "SensorDataStorage",
                        PerformanceTier = "standard",
                        ServiceObjective = "S3",
                        EntitySets = new List<EntitySet>
                        {
                            new EntitySet
                            {
                                Count = 10000,
                                EntityName = "Measurement",
                                Name = "Measurements"
                            }
                        }
                    },
                    new AzureAppService()
                    {
                        DeployTo = "Azure",
                        Name = "VisualisationApi",
                        PerformanceTier =  "StandardS3",
                        WithApplication = "VisualisationDataApiSQL"

                    },
                    new AzureAppService()
                    {
                        DeployTo = "Azure",
                        Name = "CollectingApi",
                        PerformanceTier = "StandardS3",
                        WithApplication = "DataCollectingApiSQL"

                    }
                }
            };

        }
    }
}
