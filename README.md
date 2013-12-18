nkinesis
========

Simple .NET client for Amazon Kinesis

NOTE: This was written before AWS released an updated version of their SDK that supports Kinesis.  The current version (as of December 12, 2013) now supports Kinesis.  I would recommend using the AWS SDK for writing Kinesis apps.


### Create/Delete stream:

```csharp
var kinesis = new KinesisClient(accessKey, secretKey);

var streamName = "TestStream";

var createStreamRequest = new CreateStreamRequest()
{
    StreamName = streamName,
    ShardCount = 1
};
var resp = kinesis.CreateStream(createStreamRequest);

var deleteStreamRequest = new DeleteStreamRequest()
{
    StreamName = streamName
};
var deleteStreamResponse = kinesis.DeleteStream(deleteStreamRequest);


```        
    
### Walk streams and read records:

```csharp
var listStreamRequest = new ListStreamsRequest();
var listStreamResponse = kinesis.ListStreams(listStreamRequest);
foreach (var currentStreamName in listStreamResponse.StreamNames)
{
    var describeStreamRequest = new DescribeStreamRequest() 
    { 
        StreamName = currentStreamName 
    };
    var describeStreamResponse = kinesis.DescribeStream(describeStreamRequest);

    foreach (var shard in describeStreamResponse.StreamDescription.Shards)
    {
        var getShardIteratorRequest = new GetShardIteratorRequest()
        {
            StreamName = currentStream,
            ShardId = shard.ShardId,
            ShardIteratorType = ShardIteratorTypes.TRIM_HORIZON
        };
        var getShardIteratorResponse = kinesis.GetShardIterator(getShardIteratorRequest);

        var thisShardIterator = getShardIteratorResponse.ShardIterator;
        while (thisShardIterator != null)
        {
            var getRecordsRequest = new GetRecordsRequest()
            {
                ShardIterator = thisShardIterator,
                Limit = 10
            };
            var getRecordsResponse = kinesis.GetRecords(getRecordsRequest);

            foreach (var record in getRecordsResponse.Records)
            {
                var data = Encoding.UTF8.GetString(Convert.FromBase64String(record.Data));
                Console.WriteLine(data);
            }
            thisShardIterator = getRecordsResponse.NextShardIterator;
        }
    }
}

```

### Write record:

```csharp
var request = new PutRecordRequest()
{
    StreamName = streamName,
    PartitionKey = partitionKey,
    Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(data))
};
var response = kinesis.PutRecord(request);
Console.WriteLine("Put record {0}", response.SequenceNumber);
```

