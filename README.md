
# ShopApp

Bu proje Udemy, Youtube gibi platformlardan öğrendiklerimi uygulamak amacıyla C# ve Asp.Net Core 6.0 teknolojileri kullanılarak geliştrilmiş bir bir mini e-ticaret sitesidir.




## Kullanılan Teknolojiler

ShopApp e-ticaret uygulamasını geliştirirken kullandığım teknolojiler ve yaklaşımlar;
- C#
- .Net Core Mvc 
- Code First
- Entity Framework Core
- N-Tier Architecture
- UnitOfWork
- Dependency Injection
- Fluent Validation
- Asp.Net Core Identity
- Authorization & Authentication
- Session
- Microsoft SQL Server
- Iyzico integration
gibi birçok teknoloji ve yaklaşım kullanılarak geliştirilmiştir.

  
# Login

ShopApp projesini başlattığınızda, uygulama oturum açma sayfasına yönlendirilir. Uygulama başlatıldığında, veritabanında iki kullanıcı kaydı bulunur:
- Yönetici (admin) kullanıcısı
- Müşteri (customer) kullanıcısı.
Projemizde şu anda sadece "admin" ve "customer" adlı iki kullanıcı bulunmaktadır. İsterseniz uygulamaya e-posta adresinizle kayıt olabilir ve hesabınıza gelen onay mesajını onaylayarak müşteri girişi yapabilirsiniz.

  
## 1-) Customer Login
- E-Mail : customer@shopapp.com
- Password : Shopapp123
e-mail ve password bilgileri ile login sayfasından müşteri girişi yaparak uygulamamızı inceleyebilirsiniz.

Not : Kişisel e-posta adresinizle uygulamamıza kayıt olduktan ve onay e-postasını onayladıktan sonra kendi hesabınız üzerinden müşteri girişi yapabilirsiniz. Onay mailinin gelmesi bazen 1-2 dakika almaktadır.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/c7vjutn.png)

Müşteri (customer) bilgileri ile giriş yaptıktan sonra karşımıza uygulamamızın anasayfası gelecektir.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/jm540zc.png)

Filtreleme yaparak istediğiniz ürünlere daha koaly bir şekilde ulaşabilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/mqh07xe.png)

## 1.1 Sepete Ürün Ekleme

Her bir ürün üzerindeki 'Add to Cart' butonuna tıklayarak sepetinize ürün ekleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/ngkhaq4.png)

- Eklediğiniz ürünleri sepetinizde (MyCart sekmesinden) görüntüleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/nb3yfpt.png)

## 1.2 Ödeme Sayfası
Sepetinizdeki ürünleri satın almak için Kart  ve Sipariş bilgilerini doldurarak satın alma işlemini gerçekleştirebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/13ovcsj.png)

Kart Bilgileri doğru girilmiş ise Başarılı İşlem Sayfasına yönlendirilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/4612dhu.png)

## 1.3 Siparişlerim Sayfası
Sipariş ile ilgili detayları görmek veya geçmiş siparişleri görüntülemek için Siparişlerim (Orders) sekmesi üzerinden görüntüleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/c5euxo3.png)


## 1.4 Iyzico Ödeme Altyapısı

https://sandbox-merchant.iyzipay.com/auth/login adresine eriştikten sonra, test amaçları için tasarlanmış sistemde aşağıda belirtilen bilgilerle oturum açabilirsiniz. Giriş yaptıktan sonra, "Dashboard" sekmesine giderek sipariş işlemlerini görüntüleyebilirsiniz. 
- E-mail : pobavo8586@czilou.com
- Password : 741963

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/pxdwgp8.png)

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/dwi4xls.png)

Daha fazla ayrıntı için paneldeki "Transactions" bölümüne ve ardından "All Transactions" seçeneğine tıklayarak detaylara ulaşabilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/g6vojau.png)

# 

## 2-) Admin Login
- E-Mail : admin@shopapp.com
- Password : Shopapp123
e-mail ve password bilgileri ile login sayfasından admin girişi yaparak uygulamamızı inceleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/qf0ttu5.png)
 
Admin bilgileri ile giriş yaptıktan sonra karşımıza admin için özelleştirilmiş bir anasayfa yapısı bizi karşılayacaktır.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/izd2h3a.png)


## 2.1 Ürün İşlemleri
- Ürünleri Listeleme, Ekleme, Silme, Güncelleme ve istersek ürünü aktif, pasif yaparak satışa sunulmasına müdahale edebiliriz. Ayrıca, hangi ürünlerin anasayfada listeleneceğine karar verebiliriz.

### 2.1.1 Ürün Listeleme Sayfası
Bu arayüz üzerinden ürün ekleme (Add Product) ve güncelleme (Edit) sayfalarına gidebilir, ayrıca Ürün silebilirsiniz (delete butonunu kullanarak).

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/exbfqjg.png)


### 2.1.2 Ürün Ekleme
Bu arayüz üzerinden ilgili ürün Ekleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/qzpz9qh.png)

### 2.1.3 Ürün Güncelleme
Bu arayüz üzerinden ilgili ürün güncelleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/9ycrl2x.png)

## 2.2 Kategori İşlemleri

### 2.2.1 Kategori Listeleme Sayfası
Bu arayüz üzerinden Kategori ekleme ve güncelleme sayfalarına gidebilir, ayrıca önceden eklenmiş bir kategoriyi silebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/n1712dq.png)

### 2.2.2 Kategori Ekleme
Bu arayüz üzerinden kategori Ekleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/reme57h.png)

### 2.2.3 Kategori Güncelleme
Bu arayüz üzerinden ilgili kategoriyi güncelleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/d1apoki.png)

## 2.3 Rol İşlemleri
Bu arayüz üzerinden kullanıcı rol Ekleme ve rol güncelleme arayüzüne kolayca gidebilir ve istediğiniz rolü silebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/pi84uos.png)


### 2.3.1 Rol Güncelleme & Yönetme
Bu arayüz üzerinden ilgili kullanıcıyı istediğiniz admin rölüne veya customer rolüne atayabilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/ap3t8hi.png)


## 2.4 Kullanıcı İşlemleri
Uygulamada kayıtlı olan kullanıcılar 'DataTables' kütüphanesi kullanılarak listelenmiştir. Bu arayüz üzerinden kullanıcı Ekleme , güncelleme ve silme işlemlerini gerçekleştirebilirsiniz.

Not : e-postası onaylanmamış kullanıcılar kullanıcılar tablosunda siyah olarak listelenir. Edit butonuna tıklayarak kullanıcının e-postasını manuel bir şekilde güncelleyebilirsiniz.

![Uygulama Ekran Görüntüsü](https://i.hizliresim.com/28bn8lz.png)

