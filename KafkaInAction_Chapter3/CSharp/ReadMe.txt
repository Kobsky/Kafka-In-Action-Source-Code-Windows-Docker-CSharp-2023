#Setup and use Kafka Connect to pass messages from file source to Kafka

1. Create new tmp folder in D:/kafka_2.13-3.4.0/

2. Create new empty file connect.offsets in the tmp folder

3. Change value in file connect-standalone.properties
to:
offset.storage.file.filename=D:/kafka_2.13-3.4.0/tmp/connect.offsets

4. Open CMD and run:
cd D:\kafka_2.13-3.4.0\bin\windows

5. Run: 
connect-standalone D:\kafka_2.13-3.4.0\config\connect-standalone.properties D:\Code\Kafka-In-Action-Source-Code-Windows-Docker-CSharp-2023\KafkaInAction_Chapter3\CSharp\alert-source.properties 

6. Run:
kafka-topics --bootstrap-server localhost:9092 --describe --topic kinaction_alert_connect

7. Run:
kafka-console-consumer --bootstrap-server localhost:9092 --topic kinaction_alert_connect --from-beginning