![image](https://github.com/user-attachments/assets/db402f27-e8fb-4157-a739-fb69c52e0420)

- [Solid Prensibles](#solid-prensibles)
- [Mikroservisler](#mikroservisler)
- [Mikroservis Mimarisi](#mikroservis-mimarisi)
- [HttpClient ve HttpClientFactory Kullanımı](#httpclient-ve-httpclientfactory-kullanımı)
- [Api Gateway](#api-gateway)
- [Asenkron Programlama](#asenkron-programlama)
- [MongoDb](#mongodb)
- [DTO (Data Transfer Object)](#dto-data-transfer-object)
- [AutoMapper](#automapper)
- [Singleton, Scoped ve Transient Kavramları](#singleton-scoped-ve-transient-kavramları)
- [Dapper](#dapper)
- [.Net Architectures](#net-architectures)
- [Onion Architecture](#onion-architecture)
- [Repository](#repository)
- [Unit of Work (UoW)](#unit-of-work-uow)
- [CQRS (Command Query Responsibility Segregation)](#cqrs-command-query-responsibility-segregation)
- [Mediator](#mediator)
- [Fluent Validation](#fluent-validation)
- [Saga](#saga)
- [EventBus](#eventbus)
- [Outbox Pattern](#outbox-pattern)
- [Domain-Driven Design (DDD)](#domain-driven-design-ddd)
- [Docker](#docker)
- [Identity Server](#identity-server)
- [JWT](#jwt)

## Solid Prensibles

E-ticaret uygulamasında bu prensiplerin doğru uygulanması, sistemi daha esnek ve bakımı kolay hale getirir.

- **1. Tek Sorumluluk Prensibi (Single Responsibility Principle — SRP)**

Bu prensip, her sınıfın tek bir sorumluluğu olmasını belirtir. Örneğin, ürün yönetimi için:

![image](https://github.com/user-attachments/assets/6fc59678-08f5-4ea3-8997-47ddee2354b1)

![image](https://github.com/user-attachments/assets/6983e493-0555-4835-bb09-47c2381a834e)

Yukarıdaki diyagramda görüldüğü gibi, ProductManager sınıfı artık tek bir sorumluluğa sahip: ürünlerin sisteme eklenmesi. 

Diğer sorumluluklar (fiyat hesaplama ve veritabanı işlemleri) ayrı arayüzler ve sınıflara taşınmış durumda. Ok işaretleri (-->) bağımlılık ilişkisini gösteriyor.

- **2. Açık/Kapalı Prensibi (Open/Closed Principle — OCP)**

Bu prensip, yeni özellikler eklerken mevcut kodu değiştirmeden sistemi genişletmeyi hedefler. Örneğin, indirim stratejileri için:

![image](https://github.com/user-attachments/assets/408d682f-fdf9-47b4-a049-acb6de3bc329)

![image](https://github.com/user-attachments/assets/eebca543-4cb5-4d86-a801-d25533e12856)

- **3. Liskov’un Yerine Geçme Prensibi (Liskov Substitution Principle — LSP)**

Bu prensip, alt sınıfların üst sınıflarının yerine geçebilmesi gerektiğini belirtir. E-ticaret uygulamasında farklı ürün tipleri için:

![image](https://github.com/user-attachments/assets/696c77cc-ec10-41ae-98a0-8baf2d5dda86)

![image](https://github.com/user-attachments/assets/f4b81ea4-1e68-46e0-b5eb-06bca682e461)

- **4. Arayüz Ayrımı Prensibi (Interface Segregation Principle — ISP)**

Bu prensip, istemcilerin kullanmadıkları metotları içeren geniş arayüzlere bağımlı olmamaları gerektiğini belirtir. E-ticaret uygulamasında farklı ödeme yöntemleri için:

![image](https://github.com/user-attachments/assets/67d711e6-e51e-45a9-94a2-4d0f66185d78)

![image](https://github.com/user-attachments/assets/54d8ad49-b2c4-4d20-88e2-79155e6b1898)

- **5. Bağımlılığın Ters Çevrilmesi Prensibi (Dependency Inversion Principle — DIP)**

Bu prensip, yüksek seviyeli modüllerin düşük seviyeli modüllere değil, her ikisinin soyutlamalara bağımlı olması gerektiğini belirtir. E-ticaret uygulamasında envanter yönetimi için:

![image](https://github.com/user-attachments/assets/30164787-35a4-44b5-865d-4105870c3b81)

![image](https://github.com/user-attachments/assets/ea3cba53-d1fb-4c47-b1a8-011066c1cc82)

- SRP ile her sınıfın net sorumlulukları olur
- OCP ile yeni özellikler eklerken mevcut kodu bozmadan geliştirme yapabilirsiniz
- LSP ile farklı ürün tiplerini veya ödeme yöntemlerini kolayca ekleyebilirsiniz
- ISP ile her modül sadece ihtiyaç duyduğu arayüzlere bağımlı olur
- DIP ile farklı envanter servisleri veya veri kaynaklarını kolayca değiştirebilirsiniz

## Mikroservisler

### Catalog Micro Service

**.NET 8.0 ile ASP.NET Core Web API projesi oluşturuldu.**

### Discount Micro Service

**.NET 8.0 ile ASP.NET Core Web API projesi oluşturuldu.**

### Order Micro Service

**.NET 8.0 ile Onion Architecture, CQRS, Mediator uygulanmıştır.**

- MultiShop.Order.Application
- MultiShop.Order.Domain
- MultiShop.Order.Persistance
- MultiShop.Order.WebApi

Docker üzerinden OrderDb ayağa kaldırıldı.

Docker yönetimi Portainer arayüzü üzerinden sağlandı.

**Docker Download:**

https://www.docker.com/products/docker-desktop/

**Portainer Download:**

docker volume create portainer_data

docker run -d -p 8000:8000 -p 9000:9000 --name=portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce

### Cargo Micro Service

**.NET 8.0 ile N-Tier Architecture uygulanmıştır.**

- MultiShop.Cargo.BusinessLayer
- MultiShop.Cargo.DataAccessLayer
- MultiShop.Cargo.DtoLayer
- MultiShop.Cargo.EntityLayer
- MultiShop.Cargo.WebApi

## Mikroservis Mimarisi

Mikro servis mimarisi, bir uygulamanın küçük, bağımsız çalışan servisler (hizmetler) olarak geliştirilmesini ve bu servislerin birbiriyle genellikle HTTP/REST, gRPC veya mesaj kuyrukları (RabbitMQ, Kafka) gibi hafif iletişim protokolleri üzerinden haberleşmesini sağlayan mimari yaklaşımdır. Her mikro servis belirli bir işlevi yerine getirir ve genellikle bağımsız olarak dağıtılabilir, test edilebilir ve geliştirilebilir.

![image](https://github.com/user-attachments/assets/6111c5a0-e7e3-4173-9187-fdc46cc36691)

![image](https://github.com/user-attachments/assets/cc34de45-756b-4aef-8c32-41ce3b367192)

![image](https://github.com/user-attachments/assets/8776da6f-a77e-4cba-acb3-0dc22d13ceae)

![image](https://github.com/user-attachments/assets/6bd2f7a4-6929-4390-9846-77f9a22fb418)

## HttpClient ve HttpClientFactory Kullanımı

### HttpClient

HttpClient, .NET içerisinde HTTP istekleri yapmak için kullanılan bir sınıftır. 

❗ Sorun: HttpClient'in yanlış kullanımı

Çoğu geliştirici HttpClient nesnesini her istekte yeniden oluşturur, bu da socket exhaustion (soket tükenmesi) sorununa neden olabilir. Çünkü HttpClient arka planda bağlantıları kapatmaz, TCP bağlantıları bir süre açık kalır.

### Http Client Factory

IHttpClientFactory, .NET Core 2.1+ ile gelen bir yapıdır ve HttpClient'in doğru ve verimli yönetilmesini sağlar. Dependency Injection ile birlikte çalışır.

![image](https://github.com/user-attachments/assets/2cd55a64-73de-431a-b1f3-5f5107ea0589)

✅ Sonuç

- Küçük ve tek seferlik bir uygulama yazıyorsan HttpClient kullanılabilir.
- Ancak gerçek bir uygulama, çoklu servis çağrıları ya da yük altında çalışan bir servis geliştiriyorsan, IHttpClientFactory kesinlikle tercih edilmelidir.

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

## DTO (Data Transfer Object)

- DTO(Data Transfer Object) Entity yerine kullanılır. 
- Veri alış verişi için Entity'leri kullanırsak güvenlik zafiyeti olur password gibi kolonlar taşınmak istenmeyebilir veya bazı kolonların gizlenmesi gerekebilir.
- Gizlenmesi gerektiği durumda DTO'larda gizlenmek istenen kolonlar yer almaz.
- DTO'larda farklı tablolardan gelen JOIN'lenmiş verilerde olabilir.
- JOIN için farklı nesnelerdeki(Entity)'lerin farklı kolonları DTO'ya yansıtılabilir. DTO'ya Complex Type'de denilmektedir.
- Entity'den DTO'ya - DTO'dan ise Entity'ye dönüşüm için elle eşlemek gerekir. Elle eşlemek zor ve zahmetli olduğu için .NET'de AutoMapper kütüphanesi kullanılır.

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

## Dapper

- Dapper, .NET platformu üzerinde kullanılan mikro ORM (Object-Relational Mapper) kütüphanesidir. ORM’ler, veritabanı ile nesneler arasında köprü kurarak veritabanı işlemlerini daha okunabilir ve yönetilebilir hale getirir. Dapper ise bu işi çok hafif, hızlı ve doğrudan SQL yazarak yapan bir araçtır. 

- Dapper, Stack Overflow ekibi tarafından geliştirilmiş ve System.Data.IDbConnection arayüzünü genişleten extension method'lar aracılığıyla çalışan bir kütüphanedir. ADO.NET’in sunduğu altyapıyı kullanır ama onun karmaşasını ortadan kaldırır. Sorgular SQL cümleleri olarak yazılır, Dapper bu sorguların sonuçlarını otomatik olarak C# nesnelerine eşler.

![image](https://github.com/user-attachments/assets/bba0a9b2-9538-4ff3-a14e-75a43385181a)

🔎 Özetle Ne Zaman Hangi Teknoloji?

- EF Core: Migration, change tracking, navigasyonlar, domain odaklı yapı isteniyorsa.

- Dapper: Performans öncelikliyse, SQL bilgisi varsa, basit ve hızlı bir yapı gerekiyorsa.

- NHibernate: Karmaşık ilişkiler, gelişmiş konfigürasyonlar, legacy sistemlerle uyum gerekiyorsa.

- ADO.NET: Düşük seviyede tam kontrol ve maksimum performans gerekiyorsa (örneğin bulk işlemler).

![image](https://github.com/user-attachments/assets/6ec85484-3c45-4eaf-92c5-6a1a1114d063)

![image](https://github.com/user-attachments/assets/94775af8-d567-4aac-8468-024bd06b30dc)

![image](https://github.com/user-attachments/assets/bf2254bc-a785-4483-ab02-50466e770f0e)

![image](https://github.com/user-attachments/assets/cd93f831-f71d-480a-8e85-9fa5dbde9956)

## .Net Architectures

![image](https://github.com/user-attachments/assets/7e96483e-36af-4f60-8d26-34b0f0aac613)

![image](https://github.com/user-attachments/assets/fb084a4e-f7a8-44d9-ac48-d27c42edf46c)

![image](https://github.com/user-attachments/assets/a5164d1c-b1e6-45ea-b13c-289a4d177346)

![image](https://github.com/user-attachments/assets/01cd0804-7f9e-4777-b2d5-2bdb2b82d914)

![image](https://github.com/user-attachments/assets/a6aff635-c55c-4abc-8e3f-bdfa05834135)

![image](https://github.com/user-attachments/assets/0f5efaed-1f3c-4863-af1b-3c9a0fa79e60)

![image](https://github.com/user-attachments/assets/29ab2c8d-0be8-4751-9f55-b476a4ec0027)

![image](https://github.com/user-attachments/assets/f68af4a3-cfd7-4ad1-a01b-cb6513f077e5)

Sonuç:

✅ Küçük projeler için Monolitik, MVC

✅ Orta ölçek için Katmanlı, Onion

✅ Büyük sistemler için Mikroservis, Hexagonal

✅ Gerçek zamanlı sistemler için Event-Driven

✅ Okuma/yazma yükü ayrımı için CQRS

## Onion Architecture

Onion mimarisi, N-tier katmanlı mimarinin ileride yaşatacağı sorunları çözmeyi sunmaktadır. Onion ile oyunun kurallarını değiştiren şey, Domain Layer (tabiri caizse soğan’ın cücüğü) Entity’lerin Tüm Uygulamanın Merkezinde olmasıdır. Bu, daha yüksek esneklik ve daha az bağlantı anlamına gelir. Bu yaklaşımda, tüm Katmanların yalnızca Çekirdek Katmanlara bağlı olduğunu görebiliriz.

![image](https://github.com/user-attachments/assets/8c10e9f8-7914-4d10-8ce4-2151838b0eac)

**Folder Structure:** 

![image](https://github.com/user-attachments/assets/c7fa97ae-c5ff-41ee-9178-1224fd922870)

## Repository

![image](https://github.com/user-attachments/assets/611f10aa-1227-4519-b13c-9463d4a61a5f)

![image](https://github.com/user-attachments/assets/2f9d3789-5350-4d5a-a1d6-a3a75696f738)

![image](https://github.com/user-attachments/assets/a8f34236-bc77-4938-91d2-454ac1b35a28)

![image](https://github.com/user-attachments/assets/8533c8b1-1b84-4ca4-af60-48afeb2a7c1c)

![image](https://github.com/user-attachments/assets/dbe4eddf-0a92-4ebc-8e5d-5dd80d37ca14)

![image](https://github.com/user-attachments/assets/b8e0b1f7-5e7f-4400-9a33-4b91559c6692)

## Unit of Work (UoW)

Yazılımda bir transaction'ı kapsayan işlemleri tek bir noktadan yönetmek için kullanılan bir tasarım desenidir.

🧠 Amaç:

Birden fazla repository üzerinden yapılan değişikliklerin, tek bir işlem (transaction) gibi ele alınmasını sağlar.

![image](https://github.com/user-attachments/assets/88015a8b-96b2-4f67-8069-c1a2b7bb4d79)

![image](https://github.com/user-attachments/assets/e1db471e-6810-4d0c-9a6d-dba42465094f)

![image](https://github.com/user-attachments/assets/b7c1539d-1faa-4e6d-9bef-f00e2a86444a)

![image](https://github.com/user-attachments/assets/bc103c4e-975e-46bc-9a76-649dca59a20c)

![image](https://github.com/user-attachments/assets/5f6c4472-4765-4711-b394-9778f42be9dc)

## CQRS (Command Query Responsibility Segregation)

Yazılım mimarisinde veri okuma (Query) ve veri yazma (Command) işlemlerinin farklı modellerle ele alınmasını öngören bir yaklaşımdır.

![image](https://github.com/user-attachments/assets/957cb841-f4d2-43a4-9aea-dc8229323bcd)

![image](https://github.com/user-attachments/assets/ff4e8970-d87d-456f-8187-6591b4298806)

![image](https://github.com/user-attachments/assets/88e03f41-a778-4778-b367-b0162fdff85d)

![image](https://github.com/user-attachments/assets/7d663cd4-0231-4c97-90b5-e4ff12a7c0b2)

![image](https://github.com/user-attachments/assets/257d436a-6c31-4fdc-9e3e-b64ed15bf3fd)

## Mediator

MediatR, .NET uygulamalarında "mediator" tasarım desenini uygulamak için kullanılan hafif bir kütüphanedir. 

Bu desenin amacı, nesneler arasında doğrudan iletişimi ortadan kaldırarak, iletişimi merkezi bir yapı üzerinden gerçekleştirmektir.

**MediatR kütüphanesi sayesinde:**

- Bir işlemi (örneğin bir komutu ya da sorguyu) bir request (istek) olarak tanımlarsınız.
- Bu isteği işleyen bir handler (işleyici) oluşturursunuz.
- Uygulama içinde isteği doğrudan handler’a iletmezsiniz. Bunun yerine, MediatR üzerinden gönderirsiniz. MediatR uygun handler’ı bulur ve çalıştırır.

![image](https://github.com/user-attachments/assets/3a3ced4f-ee84-4f2d-9a9a-e7308d7d470e)

![image](https://github.com/user-attachments/assets/58f06b20-7f7d-4da7-84a6-ad310064f0e7)

![image](https://github.com/user-attachments/assets/7ae6fa4d-fbf5-436a-bb9f-c015c454dc2a)

## Fluent Validation

FluentValidation, C# projelerinde özellikle DTO veya Command sınıflarına yönelik olarak geliştirilen tip güvenli, okunabilir, esnek bir validation (doğrulama) kütüphanesidir.

👉 Amacı, kuralları model sınıfından ayırmak ve clean code prensiplerine uymaktır.

![image](https://github.com/user-attachments/assets/8c5ca9d1-01f3-4321-96a3-f721f31b3f8d)

![image](https://github.com/user-attachments/assets/7f526b57-29dc-4a6f-949a-e7964a3c493d)

![image](https://github.com/user-attachments/assets/7a5579aa-e7dd-41a3-b2cd-5ff251d74e8e)

## Saga 

Saga Pattern, mikroservis mimarilerinde dağıtık işlemleri yönetmek amacıyla kullanılan bir desen (pattern)'dir. 
 
Özellikle birden fazla servis arasında koordineli işlemler yapıldığında, bu işlemlerin ya tamamen başarılı olması ya da hatalı durumda geri alınabilmesi (rollback/compensate) gerektiğinde devreye girer.

Bir işlemi küçük adımlara (transaction'lara) böler. Her adım bir mikroservis tarafından gerçekleştirilir. 

Her adım başarılı olursa bir sonraki adıma geçilir. Eğer herhangi bir adımda hata oluşursa, önceki adımlar için telafi (compensating) işlemleri çalıştırılarak sistem tutarlı hale getirilir.

![image](https://github.com/user-attachments/assets/e0dba02e-3e32-4a75-9c96-893c1cb3ed7f)

![image](https://github.com/user-attachments/assets/03b8b58b-3182-4337-88d6-a035466a476e)

![image](https://github.com/user-attachments/assets/10cd2b64-1ee2-40a9-9175-bf7f5ae8d0ec)

![image](https://github.com/user-attachments/assets/5b664e5b-ea6a-4fcf-a797-191049efa6f7)

![image](https://github.com/user-attachments/assets/f88a5c61-cf4d-444c-b45e-ae7f1cefb1b4)

![image](https://github.com/user-attachments/assets/57aa559a-4577-4edd-b892-589d7b117664)

![image](https://github.com/user-attachments/assets/ab27205d-5661-4bca-8248-8b525751e7d8)

**Eğer bir e-ticaret uygulamasında:**

- Sipariş → Ödeme → Stok → Kargo gibi ardışık, dağıtık adımlar varsa,
- Ve bu adımların ya tamamı başarılı olmalı ya da sistem geri alınmalıysa,
- Saga Pattern tam da bu senaryolar için idealdir. Klasik transaction yapısının yapamadığını dağıtık bir şekilde gerçekleştirmeni sağlar.

## EventBus

EventBus, bir servis içinde veya servisler arasında meydana gelen olayları (events) yayınlama (publish) ve bu olaylara abonelik (subscribe) sistemi ile başka servislerin tepki verebilmesini sağlar.

![image](https://github.com/user-attachments/assets/58e08d61-cd3b-4dce-8650-d2f0147909ab)

![image](https://github.com/user-attachments/assets/892faa35-5b85-47f6-9b05-f3c7d791958a)

![image](https://github.com/user-attachments/assets/d63db774-7a3a-4252-b97b-69bbb89f6acc)

EventBus, sistemde bir şey olduğunda (örneğin "sipariş oluşturuldu") bu olayı ilgilenen tüm servislere bildiren bir mekanizmadır. 

Mikroservislerde servisler arası haberleşme, asenkron işlem akışı, ve özellikle Saga Pattern uygulamak için olmazsa olmaz bir yapıdır.

## Outbox Pattern

Outbox Pattern, mikroservis mimarisinde veri tutarlılığı ve event güvenliği sağlamak için kullanılan bir desendir. 

Özellikle event-driven (olay tabanlı) mimarilerde, bir işlemi yaptıktan sonra bu işlemi bildiren bir event'in kesinlikle gönderilmesini garanti etmek için kullanılır.

![image](https://github.com/user-attachments/assets/e612095b-48e3-48cb-b43f-4b7f995ee7d7)

![image](https://github.com/user-attachments/assets/01141bf9-5d73-4002-92e3-7484a8ef2552)

![image](https://github.com/user-attachments/assets/6c9221cd-606c-48f4-a39e-5dabed3f7b8a)

**Outbox Pattern, veritabanı işlemleriyle event üretimini aynı transaction’a alarak:**

- Veri tutarlılığını sağlar
- Event kaybını önler
- Event-driven mimaride güvenilirliği artırır

## Domain-Driven Design (DDD)

Domain-Driven Design (DDD), karmaşık yazılım sistemlerinin, iş alanı (domain) ve o domain’e ait uzmanlık (business knowledge) etrafında modellenmesini sağlayan bir yazılım tasarım yaklaşımıdır.

- Domain = Uygulamanın çalıştığı iş alanı. Örneğin: E-Ticaret, Bankacılık, Lojistik, Sağlık, vb.
- DDD’de bu iş alanı, yazılımın merkezi olur.

![image](https://github.com/user-attachments/assets/8a16e8b7-6912-43cf-9a4e-69e329fe7c72)

![image](https://github.com/user-attachments/assets/a5bfc168-b4b8-411d-a61d-8228849afc8e)

![image](https://github.com/user-attachments/assets/7215b853-20a0-4c67-b3be-4c324125f5ac)

![image](https://github.com/user-attachments/assets/9f500d73-78bb-46be-8b14-a0ca55fd0a43)

## Docker

![image](https://github.com/user-attachments/assets/f4e9778a-0b7c-475a-a6c0-750a1482e050)

![image](https://github.com/user-attachments/assets/ddf3b161-6f01-4c28-8ef5-c901fea07808)

![image](https://github.com/user-attachments/assets/c954901d-d587-49b2-904c-77aac36dccd7)

![image](https://github.com/user-attachments/assets/cf9eda24-1c3e-41ae-a3a1-6607a20d85ea)

🐳 Ne Zaman Docker Kullanılmalı?

![image](https://github.com/user-attachments/assets/5226ba48-4fc5-41ce-b7ff-8c01764845e8)

🖥️ Ne Zaman VM (Sanal Makine) Kullanılmalı?

![image](https://github.com/user-attachments/assets/ed334714-33ce-467c-8031-ca67b7f9a921)

### Portainer

- Portainer, Docker ve Kubernetes gibi konteyner orkestrasyon platformlarının yönetimini kolaylaştıran bir açık kaynaklı web tabanlı yönetim aracıdır.
- Portainer, Docker ortamlarını ve Kubernetes kümelerini görsel bir arayüz üzerinden yönetmenizi sağlar. Yani, Docker konteynerlerini ve Kubernetes pod’larını terminal komutları yazmak yerine grafiksel bir arayüz üzerinden yönetmek için kullanılır.

### DBeaver

**DBeaver Download:**

https://dbeaver.io/

- DBeaver, veritabanı yönetimi ve SQL geliştirme için kullanılan popüler bir açık kaynaklı, çok platformlu (Windows, macOS, Linux) veritabanı istemcisi ve yönetim aracıdır.
- DBeaver, SQL sorguları yazmak, veritabanlarını yönetmek, verileri görselleştirmek ve daha birçok veritabanı işlemini kolayca gerçekleştirmek için kullanılan güçlü bir araçtır.
- DBeaver, relational (ilişkisel) ve non-relational (ilişkisiz) birçok veritabanı sistemini destekler.
- DBeaver’ın en büyük avantajlarından biri, çoklu veritabanı sistemlerini tek bir arabirimde desteklemesidir.

## Identity Server

**IdentityServer4, OpenId Connect (Authentication) ve OAuth 2.0 (Authorization) protokollerini implement eden , uygulayan bir frameworktür.** 

- Identity Server, kimlik doğrulama (authentication) ve yetkilendirme (authorization) işlemlerini merkezi olarak yönetmek için kullanılan bir açık kaynaklı framework'tür.
- .NET platformu üzerinde geliştirilmiştir ve genellikle OAuth 2.0 ve OpenID Connect protokollerini uygular.

![image](https://github.com/user-attachments/assets/49197fa1-a2d1-4a53-a49f-5e8744fda0d5)

![image](https://github.com/user-attachments/assets/e02243d8-e20a-4944-a2b4-11029cc9d0db)

🎯 Ne Zaman Kullanmalısın?

- Uygulamalar arasında SSO (Single Sign-On) ihtiyacı varsa
- Birden fazla istemci türü varsa (web, mobil, api)
- Mikroservis mimarisi kullanıyorsan ve merkezi authentication yönetimi gerekiyorsa
- OAuth2 / OIDC standartlarına uyan bir yapı gerekiyorsa

- **OAuth 2.0, “neye erişebilirim?” sorusunu çözer.**
- **OpenID Connect, “kim bu kullanıcı?” sorusunu cevaplar.**

OAuth 2.0, kullanıcıların kimlik bilgilerini paylaşmadan, bir uygulamanın başka bir kaynağa (API, servis vb.) sınırlı erişim yetkisi almasını sağlayan bir yetkilendirme protokolüdür.

![image](https://github.com/user-attachments/assets/320bce1f-4f0e-40e1-bb47-1822f7ab4432)

OpenID Connect (OIDC), OAuth 2.0 üzerine inşa edilmiş bir kimlik doğrulama (authentication) protokolüdür. Yani OAuth 2.0’ın yetkilendirme (authorization) mekanizmasını kullanarak kullanıcının kim olduğunu da doğrulamanı sağlar.

![image](https://github.com/user-attachments/assets/8ade432e-c07b-44fb-a0c4-ca8cd55794ae)

## JWT 

JWT (JSON Web Token), RFC 7519 standardına göre tanımlanmış, JSON formatında veri taşıyan, genelde kimlik doğrulama ve yetkilendirme amacıyla kullanılan dijital olarak imzalanmış bir token yapısıdır.

**JWT şu üç parçadan oluşur ve bu üç parça nokta (.) ile birbirinden ayrılır:**

xxxxx.yyyyy.zzzzz

Header.Payload.Signature

1️⃣ Header

{
  "alg": "HS256",
  "typ": "JWT"
}

alg: Kullanılan imzalama algoritmasıdır. Örneğin HS256 (HMAC-SHA256), RS256 (RSA + SHA256).

typ: Token tipi. Her zaman JWT.

Header base64url ile encode edilir.

2️⃣ Payload (Veri)

{
  "sub": "1234567890",
  "name": "Hasan",
  "email": "hasan@example.com",
  "role": "Admin",
  "iat": 1714000000,
  "exp": 1714032000
}

Payload, token'ın içerdiği verilerdir. Bunlara claim denir. Üç tür claim vardır:

📦 Claim Türleri

![image](https://github.com/user-attachments/assets/10c443a3-d85d-4d89-b151-49c69475a2e2)

3️⃣ Signature

HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret
)

- İmzanın amacı, token’ın değiştirilip değiştirilmediğini doğrulamaktır.
- Eğer biri payload’ı değiştirirse, imza uyuşmaz ve token geçersiz olur.

Signature doğrulaması yapılmadan, token'a asla güvenilmemelidir.

### 🔐 JWT Nasıl Çalışır?

1. Kullanıcı giriş yapar (username + password).
2. Server, bilgileri doğrular → JWT oluşturur → kullanıcıya döner.
3. Kullanıcı, bu token'ı sonraki tüm isteklerde HTTP header ile gönderir:
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR...
4. Sunucu gelen token’ı doğrular (signature + exp süresi).
5. Token içindeki claim’lere göre yetkilendirme yapılır.

### ⚙️ JWT’nin Avantajları

✅ Stateless: Server session tutmaz.

✅ Hızlı: Her istek için DB sorgusu yapmadan kullanıcı bilgileri token’dan okunur.

✅ Uygulamalar arası taşınabilir: Web, mobil, desktop hepsi kullanabilir.

✅ Mikroservis uyumlu.

### ⚠️ JWT Kullanırken Dikkat Edilecekler

- Token içinde hassas veri (şifre, kredi kartı, vs.) taşıma!
- Exp süresi kısa tutulmalı. Uzun süreli token yerine Refresh Token yapısı kullanılmalı.
- Refresh Token kullanıyorsan, onları güvenli storage (örneğin cookie + HttpOnly) ile koru.
- Eğer istemci ile veri taşıyorsan, HTTPS zorunlu.
- Logout senaryosunda JWT hemen geçersiz kılınamaz. Bunun için blacklist ya da short-lived token + refresh token sistemi gerekir.

### Access Token:

Access Token, kullanıcının kimliğini doğruladıktan sonra, istemcinin (client) API’lere erişebilmesi için verilen kısa ömürlü bir tokendır.

![image](https://github.com/user-attachments/assets/3878461f-6878-4412-8f9d-89b4417dcf80)

### Refresh Token:

Refresh Token, access token süresi dolduğunda, kullanıcıyı yeniden giriş yaptırmadan yeni bir access token almak için kullanılır.

![image](https://github.com/user-attachments/assets/751f6985-2991-4f8e-8c7b-882feee66099)

### 🆚 Access Token vs Refresh Token Farkları

![image](https://github.com/user-attachments/assets/4b73ab99-c93f-4ce1-b3c3-38113a3a1afa)

### 🔍 Neden Hem Access Hem Refresh Token Kullanılır?

1. Güvenlik vs Kullanılabilirlik Dengesi
   
- Access token kısa süreli olmalı ki çalınırsa etkisi sınırlı olsun.
- Ama kullanıcıya sürekli tekrar giriş yaptıramazsın.

Bu yüzden:

- Access token kısa ömürlü
- Refresh token uzun ömürlü

→ İkili yapı ile hem güvenlik hem kullanıcı deneyimi sağlanır.

2. Logout / Blacklist Senaryosu

- Refresh token authentication server’da saklanabilir.
- Böylece istenirse token iptal edilebilir (revocation).

JWT access token'lar stateless olduğundan doğrudan iptal edilemez.

3. Token Yenileme Mekanizması

Access token süresi dolduğunda kullanıcıya hiçbir şey hissettirmeden yeni token alınabilir.

### 👨‍💻 .NET Web API'de Senaryo

1. Kullanıcı giriş yapar → JWT Access Token + Refresh Token verilir.
2. Access Token 15 dakika geçerli.
3. Süresi dolarsa, istemci Refresh Token ile yeni Access Token alır.
4. Refresh Token 7 gün geçerli, süre sonunda tekrar login gerekir.
5. Refresh Token çalınırsa → server tarafında revoke edilir.

### ⚠️ Güvenlik Uyarıları

- Refresh Token'ı localStorage gibi açık alanlarda saklama!
- Web uygulamalarında HttpOnly ve Secure Cookie önerilir.
- Refresh Token kullanımında CSRF saldırılarına dikkat edilmelidir.
- Token üretiminde exp, iat, jti (unique id) gibi claim'ler kullanılmalı.

**.NET Core’da JWT ile kimlik doğrulama yapmak için genellikle:**

- Microsoft.AspNetCore.Authentication.JwtBearer paketi kullanılır.
- AddAuthentication().AddJwtBearer(...) ile konfigürasyon yapılır.
- Authorize attribute'u ile token doğrulama istenir.
- Token üretimi için JwtSecurityTokenHandler sınıfı kullanılır.















































