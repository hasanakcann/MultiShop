![image](https://github.com/user-attachments/assets/db402f27-e8fb-4157-a739-fb69c52e0420)

- [Mikroservis Mimarisi](#mikroservis-mimarisi)
- [Api Gateway](#api-gateway)
- [Asenkron Programlama](#asenkron-programlama)
- [MongoDb](#mongodb)
- [Dto (Data Transfer Object)](#dto-data-transfer-object)
- [AutoMapper](#automapper)
- [Singleton, Scoped ve Transient Kavramları](#singleton-scoped-ve-transient-kavramları)

## Mikroservis Mimarisi

Mikro servis mimarisi, bir uygulamanın küçük, bağımsız çalışan servisler (hizmetler) olarak geliştirilmesini ve bu servislerin birbiriyle genellikle HTTP/REST, gRPC veya mesaj kuyrukları (RabbitMQ, Kafka) gibi hafif iletişim protokolleri üzerinden haberleşmesini sağlayan mimari yaklaşımdır. Her mikro servis belirli bir işlevi yerine getirir ve genellikle bağımsız olarak dağıtılabilir, test edilebilir ve geliştirilebilir.

![image](https://github.com/user-attachments/assets/6111c5a0-e7e3-4173-9187-fdc46cc36691)

![image](https://github.com/user-attachments/assets/cc34de45-756b-4aef-8c32-41ce3b367192)

![image](https://github.com/user-attachments/assets/8776da6f-a77e-4cba-acb3-0dc22d13ceae)

![image](https://github.com/user-attachments/assets/6bd2f7a4-6929-4390-9846-77f9a22fb418)

## Api Gateway

![image](https://github.com/user-attachments/assets/7c022acf-b5d0-41f6-bd1c-cecbf02f1310)

![image](https://github.com/user-attachments/assets/6aa32cf3-fd08-4fa2-891f-989b24fce24e)

![image](https://github.com/user-attachments/assets/12c20149-f889-4801-9d87-3f0205b7b53e)

## Asenkron Programlama

Asenkron programlama, özellikle uzun süren işlemler sırasında (veritabanı sorgusu, dosya okuma, HTTP isteği vs.) uygulamanın bloklanmasını önlemek için kullanılır. Temel amaç, işlemin tamamlanmasını beklerken uygulamanın yanıt vermeye devam etmesini sağlamaktır.

![image](https://github.com/user-attachments/assets/0845f8f5-f129-4f8e-84dd-3009b0298b2c)

## MongoDb

MongoDb, açık kaynaklı bir NoSQL (Not Only SQL) veritabanıdır. Doküman tabanlı bir veritabanı sistemidir ve verileri JSON-benzeri, yani BSON (Binary JSON) formatında saklar. Bu, MongoDb’nin yapısal olmayan veya yarı yapısal verileri çok verimli bir şekilde depolamasına olanak tanır. MongoDb, SQL tabanlı veritabanlarından farklı olarak, veritabanı tabloları yerine koleksiyonlar kullanır ve her bir koleksiyon içinde çok daha esnek veri yapıları bulunabilir.

![image](https://github.com/user-attachments/assets/dc44a259-9192-43d0-ac6b-46542f8c90cf)

- MongoDb ilişkisel veritabanı olmadığı için id string tutulur ve guid değer atanır.
- [BsonId] -> id olduğu belirtir.
- [BsonRepresentation(BsonType.ObjectId)] -> benzersiz olduğunu belirtir.
- [BsonIgnore] MongoDb için belge ve nesne eşleştirme işlemlerinde kullanılan bir özniteliktir. 
Bu öznitelik, belirli bir özelliğin MongoDb belgesine dönüştürülürken veya belgeden nesneye dönüştürülürken dikkate alınmamasını sağlar, yani MongoDb tarafında depolanmaz veya yüklenmez. 
[BsonIgnore] kullanmazsanız, Category özelliği MongoDb belgesine dönüştürülürken dikkate alınacak ve belgelere dahil edilecektir. Ancak, Category özelliği veri tabanında bir alanı temsil etmiyor, bu nedenle MongoDb'de gereksiz bir alana dönüşecektir. 
Bu durum, gereksiz veri saklamak ve gereksiz bellek kullanımına neden olabilir. Ayrıca, Category özelliğinin değerlerini güncelleme veya sorgulama gibi işlemleri gerçekleştirmek istediğinizde, MongoDb tarafında ekstra iş yükü oluşturabilir.

## Dto (Data Transfer Object)

DTO(Data Transfer Object) Entity yerine kullanılır. Veri alış verişi için Entity'leri kullanırsak güvenlik zafiyeti olur password gibi kolonlar taşınmak istenmeyebilir veya bazı kolonların gizlenmesi gerekebilir. Gizlenmesi gerektiği durumda DTO'larda gizlenmek istenen kolonlar yer almaz. DTO'larda farklı tablolardan gelen JOIN'lenmiş verilerde olabilir. JOIN için farklı nesnelerdeki(Entity)'lerin farklı kolonları DTO'ya yansıtılabilir. DTO'ya Complex Type'de denilmektedir. Entity'den DTO'ya - DTO'dan ise Entity'ye dönüşüm için elle eşlemek gerekir. Elle eşlemek zor ve zahmetli olduğu için .NET'de AutoMapper kütüphanesi kullanılır.

## AutoMapper

- Mapping işlemi contructor içerisinde gerçekleşir. Mapping işlemi entitylerden nesne örnekleri oluşturmak yerine (new ile örneklemek yerine), entitylerin propertyleri ile dto daki propertylerin eşleştirilmesini sağlar. AutoMapper kullanarak DTO sınıflarını oluşturduğumuzda, client tarafında göstermek istediğimiz alanları sınırlandırarak, gerçek nesnemizin güvenliğini sağlamış oluruz.

![image](https://github.com/user-attachments/assets/c10ba3f7-e843-40b0-9985-d98a3af26a54)

## Singleton, Scoped ve Transient Kavramları

.NET’te Singleton, Scoped ve Transient kavramları, uygulama içerisindeki servislerin ömür süresini (lifetime) tanımlar. Dependency Injection (DI) kullanılırken bu yaşam süreleri, nesnelerin ne zaman oluşturulacağı ve ne kadar süreyle kullanılacağı üzerinde tam kontrol sağlar.

* Singleton Nedir? 
Singleton bir tasarım şablonudur. Bellekte bir nesneden sadece bir tane olabilir. 
Her istek geldiğinde o nesne(aynı nesne) verilir. Program sonlanana kadar bellekte varlığını korur.

* Scoped Nedir?
Scoped bir nevi Prototype tasarım şablonudur. 
AddScoped nesne örneği oluşturur. Her istek geldiğinde istek başına bir tane nesne üretilir. 
Her seferinde yeni nesne üretildiği için performansı olumsuz etkileyebilir. 
İstek tamamlanmadan başka bir istek gelirse yenisi yaratılmadan zaten var olan verilir.

* Transient Nedir? 
Transient bir nevi Prototype tasarım şablonudur. 
Her istek geldiğinde istek başına bir tane nesne üretilir. 
Her seferinde yeni nesne üretildiği için performansı olumsuz etkileyebilir. 
İstek tamamlanmadan başka bir istek gelirse yenisi yaratılır eskisi verilmez. Transient ile Scoped farkı budur.

![image](https://github.com/user-attachments/assets/e7e1c8c5-5b23-4dd0-b4ef-bf01e44a7004)

![image](https://github.com/user-attachments/assets/619aebab-1556-494a-ad5d-9bc54ce74792)
