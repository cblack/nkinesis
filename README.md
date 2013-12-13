nkinesis
========

Simple .NET client for Amazon Kinesis


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
            StreamName = streamName,
            ShardId = shard.ShardId,
            ShardIteratorType = ShardIteratorTypes.TRIM_HORIZON
        };
        var getShardIteratorResponse = kinesis.GetShardIterator(getShardIteratorRequest);

        var getNextRecordsRequest = new GetNextRecordsRequest() 
        { 
            ShardIterator = getShardIteratorResponse.ShardIterator, 
            Limit = 10 
        };
        var getNextRecordsResponse = kinesis.GetNextRecords(getNextRecordsRequest);
        
        foreach (var record in getNextRecordsResponse.Records)
        {
            var data = Encoding.UTF8.GetString(Convert.FromBase64String(record.Data));
            Console.WriteLine(data);
        }
    }
}

```

