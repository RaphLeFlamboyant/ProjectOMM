// See https://aka.ms/new-console-template for more information

using MailMigrationBackend.Enums;
using MailMigrationBackend.IO;

Console.WriteLine("Hello, World!");

// JSON to SqLite
/*
var jsonAddress = "bKuYHf.qmiGjmGjqlDQW@WKEIbt.pl";

var ioFactory = new IoFactory();

var jsonReader = ioFactory.GetReader(SourceType.JSON);
var sqLiteWriter = ioFactory.GetWriter(SourceType.SQLITE);

var mailboxInternalModel = jsonReader.ReadMailbox(jsonAddress);
sqLiteWriter.SaveMailbox(mailboxInternalModel);

var sqLiteReader = ioFactory.GetReader(SourceType.SQLITE);
var savedMailbox = sqLiteReader.ReadMailbox(jsonAddress);
*/

// SqLite to JSON

var jsonAddress = "customer4@merely.mail";

var ioFactory = new IoFactory();

var sqLiteReader = ioFactory.GetReader(SourceType.SQLITE);
var jsonWriter = ioFactory.GetWriter(SourceType.JSON);

var mailboxInternalModel = sqLiteReader.ReadMailbox(jsonAddress);
jsonWriter.SaveMailbox(mailboxInternalModel);

var jsonReader = ioFactory.GetReader(SourceType.JSON);
var savedMailbox = jsonReader.ReadMailbox(jsonAddress);

Console.ReadLine();