#Setup and use Kafka Connect to pass messages from file source to Kafka

1. Create new tmp folder in D:\WslShared\kafka_2.13-3.4.0
2. Create new empty file connect.offsets in the tmp folder
3. Change value in file connect-standalone.properties
	to:
```
		offset.storage.file.filename=/mnt/d/WslShared/kafka_2.13-3.4.0/tmp/connect.offsets
		plugin.path=/mnt/d/WslShared/kafka_2.13-3.4.0/libs/connect-file-3.4.0.jar
```

4. Open Ubuntu console and run:
```
	$ connect-standalone.sh /mnt/d/WslShared/kafka_2.13-3.4.0/config/connect-standalone.properties /mnt/d/WslShared/Kafka-In-Action-Source-Code-Windows-Docker-CSharp-2023/KafkaInAction_Chapter3/CSharp/alert-source.properties 
	$ kafka-topics.sh --bootstrap-server localhost:9092 --describe --topic kinaction_alert_connect
	$ kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic kinaction_alert_connect --from-beginning
```

#Sink alerts from Kafka to file
1. Run:
```
	$ connect-standalone.sh /mnt/d/WslShared/kafka_2.13-3.4.0/config/connect-standalone.properties /mnt/d/WslShared/Kafka-In-Action-Source-Code-Windows-Docker-CSharp-2023/KafkaInAction_Chapter3/CSharp/alert-source.properties /mnt/d/WslShared/Kafka-In-Action-Source-Code-Windows-Docker-CSharp-2023/KafkaInAction_Chapter3/CSharp/alert-sink.properties
```

2. In folder D:\WslShared\Kafka-In-Action-Source-Code-Windows-Docker-CSharp-2023\KafkaInAction_Chapter3\CSharp\ 
	Should appear new file alert-sink.txt, with full messages from the topic within

#Setup and use Kafka Connect to pass data from SqlLite

1. Install the connector using Confluent Hub
```
	$ sudo confluent-hub install confluentinc/kafka-connect-jdbc:latest
```

2. To brifly understood SqlLite read the "Getting Started" from https://sqlite.org/cli.html and execute the next exercize

```
		$ sqlite3 ex1
		SQLite version 3.36.0 2021-06-18 18:36:39
		Enter ".help" for usage hints.
		sqlite> create table tbl1(one text, two int);
		sqlite> insert into tbl1 values('hello!',10);
		sqlite> insert into tbl1 values('goodbye', 20);
		sqlite> select * from tbl1;
		hello!|10
		goodbye|20
		sqlite>
```
		
	After these manipulations you will see new file ex1.db in sqlite folder
		
3. Create new Database File kafkatest.db
	Run:	
```
		$ sqlite3 kafkatest.db
		sqlite> CREATE TABLE invoices( id INT PRIMARY KEY NOT NULL, title TEXT NOT NULL, details CHAR(50), billedamt REAL, modified TIMESTAMP DEFAULT (STRFTIME('%s', 'now')) NOT NULL );
		sqlite> INSERT INTO invoices (id,title,details,billedamt)  VALUES (1, 'book', 'Franz Kafka', 500.00 );
		sqlite> select * from invoices;
```

4. Copy kafkatest.db to ..\KafkaInAction_Chapter3\CSharp folder (or use existing)

5. Create new file kafkatest-sqlite.properties in ..\KafkaInAction_Chapter3\CSharp folder (or use existing)
```
	name=kinaction-test-source-sqlite-jdbc-invoice
	connector.class=io.confluent.connect.jdbc.JdbcSourceConnector
	tasks.max=1
	connection.url=jdbc:sqlite:/mnt/d/WslShared/Kafka-In-Action-Source-Code-Windows-Docker-CSharp-2023/KafkaInAction_Chapter3/CSharp/kafkatest.db
	mode=incrementing
	incrementing.column.name=id
	incrementing.column.initial=0
	topic.prefix=kinaction-test-sqlite-jdbc-
```
6. Change value in file connect-standalone.properties
	to:
```
		plugin.path=/usr/share/confluent-hub-components
```

7. Run:
```
	$ connect-standalone.sh /mnt/d/WslShared/kafka_2.13-3.4.0/config/connect-standalone.properties /mnt/d/WslShared/Kafka-In-Action-Source-Code-Windows-Docker-CSharp-2023/KafkaInAction_Chapter3/CSharp/kafkatest-sqlite.properties 
	$ kafka-topics.sh --list --bootstrap-server localhost:9092 
	$ kafka-topics.sh --bootstrap-server localhost:9092 --describe --topic kinaction-test-sqlite-jdbc-invoices
	$ kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic kinaction-test-sqlite-jdbc-invoices --from-beginning
```

