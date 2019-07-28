create Database FaturaTakip

use FaturaTakip

Create TAble Departman
(DNo tinyint primary key,
 DAdi varchar(50),
)
--Departman Tablosu DNo kolonundan Kullanıcılar tablosuna bağlanacak.
Create table Kullanicilar(
KNo int identity(1,1) primary key,
Adi varchar(40),
Soyadi varchar(40),
DepartmanNo tinyint,
KAdi varchar(30),
Sifre nvarchar(30)
);
-- KNo Fatura Tablosu ile bağlanacak

Create Table EksikBilgi(
id tinyint primary key,
FatNo varchar(20),
HataKodu tinyint
);

--FaturaNo Fatura Tablosundan Fatno ile Bağlanacak
Create Table HataTuru(
HKodu tinyint primary key,
HataAdi varchar(70)
);

Create Table Firmalar(
FId int identity(1,1) primary key,
FirmaAdi varchar(70)
)
--FirmaId ile Fatura Tablosu bağlanacak


Create Table Fatura(
FaturaNo varchar(20) primary key,
GonderimTarihi date,
Aciklama varchar(70),
PdfYolu varchar(100),
İncelendiMi bit Default(0),
isVisible bit Default(1),
FirmaId int,
FaturaTarihi date,
KullaniciNo int,
BilgisayarAdi varchar(20)
);

Alter Table Kullanicilar 
Add Constraint FK_Kullanicilar_Departman
Foreign Key (DepartmanNo) References Departman(DNo);

Alter Table Fatura
Add Constraint FK_Fatura_Kullanicilar
Foreign Key (KullaniciNo) References Kullanicilar(Kno)

Alter TAble EksikBilgi
Add Constraint FK_EksikBilgi_Fatura_FatNo
Foreign Key (FatNo) REferences Fatura(FaturaNo);

Alter Table Fatura
Add Constraint FK_Fatura_Firmalar_FirmaId
Foreign Key (FirmaId) References Firmalar(FId)

Alter Table EksikBilgi
Add Constraint FK_EksikBilgi_HataTuru_HataKodu
Foreign Key (HataKodu) References HataTuru(HKodu)

