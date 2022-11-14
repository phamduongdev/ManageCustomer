--create database MyOrder
use MyOrder
create table dbo.tblUser (
Username nvarchar(50) NOT NULL,
Pass int NOT NULL
)
INSERT INTO [dbo].[tblUser]([Username],[Pass])VALUES(N'duong',1)
INSERT INTO [dbo].[tblUser]([Username],[Pass])VALUES(N'duyen',2)
INSERT INTO [dbo].[tblUser]([Username],[Pass])VALUES(N'huy',3)
INSERT INTO [dbo].[tblUser]([Username],[Pass])VALUES(N'hung',4)
create table dbo.tblKhachHang (
MaKH nvarchar(10) primary key,
TenKH nvarchar(50) NOT NULL,
GT bit,
Diachi nvarchar(50) NOT NULL,
NgaySinh smalldatetime
)
INSERT INTO [dbo].[tblKhachHang]([MaKH],[TenKH],[GT],[Diachi],[NgaySinh])VALUES(N'KH01',N'Duong',N'True',N'Phù Yên',29/06/2022)
INSERT INTO [dbo].[tblKhachHang]([MaKH],[TenKH],[GT],[Diachi],[NgaySinh])VALUES(N'KH02',N'Huy',N'True',N'Phù Yên',30/06/2022)
create table dbo.tblHoaDon (
MaHD numeric(18,0) primary key,
MaKH nvarchar(10) NOT NULL,
NgayHD smalldatetime NOT NULL

foreign key(MaKH) references dbo.tblKhachHang(MaKH)
)
INSERT INTO [dbo].[tblHoaDon]([MaHD],[MaKH],[NgayHD])VALUES(1, N'KH01', 29/06/2022)
INSERT INTO [dbo].[tblHoaDon]([MaHD],[MaKH],[NgayHD])VALUES(2, N'KH02', 30/06/2022)
create table dbo.tblDonViTinh (
DVT nvarchar(50) primary key
)
INSERT INTO [dbo].[tblDonViTinh]([DVT])VALUES(N'Chiếc')
INSERT INTO [dbo].[tblDonViTinh]([DVT])VALUES(N'Con')
INSERT INTO [dbo].[tblDonViTinh]([DVT])VALUES(N'Cái')
INSERT INTO [dbo].[tblDonViTinh]([DVT])VALUES(N'Thanh')
create table dbo.tblCategory (
CategoryId int primary key,
CategoryName nvarchar(50) NOT NULL
)
INSERT INTO [dbo].[tblCategory]([CategoryId],[CategoryName])VALUES(1, N'Bàn phím')
INSERT INTO [dbo].[tblCategory]([CategoryId],[CategoryName])VALUES(2, N'Chuột')
INSERT INTO [dbo].[tblCategory]([CategoryId],[CategoryName])VALUES(3, N'Màn hình')
INSERT INTO [dbo].[tblCategory]([CategoryId],[CategoryName])VALUES(4, N'Ram')
INSERT INTO [dbo].[tblCategory]([CategoryId],[CategoryName])VALUES(5, N'Khác')
create table dbo.tblMatHang (
MaHang nvarchar(10) primary key,
TenHang nvarchar(50) NOT NULL,
DVT nvarchar(50) NOT NULL,
Gia real NOT NULL,
CategoryId int NOT NULL

foreign key(DVT) references dbo.tblDonViTinh(DVT),
foreign key(CategoryId) references dbo.tblCategory(CategoryId)
)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia],[CategoryId])VALUES(N'K01', N'Bàn phím', N'Chiếc', 200000, 1)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia],[CategoryId])VALUES(N'K02', N'Bàn phím ảo', N'Chiếc', 135000, 1)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia],[CategoryId])VALUES(N'M01', N'Chuột quang', N'Con', 82000, 2)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia],[CategoryId])VALUES(N'M02', N'Màn hình hỏng', N'Cái', 10000, 3)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia],[CategoryId])VALUES(N'R01', N'Ram Kington 4GB', N'Thanh', 17000, 4)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia],[CategoryId])VALUES(N'G01', N'Giấy khen', N'Cái', 17000, 5)

create table dbo.tblCart (
MaHang nvarchar(10) primary key,
TenHang nvarchar(50),
DVT nvarchar(50),
Gia real,
Soluong int
)
create table dbo.tblChiTietHD (
MaChiTietHD numeric(18,0) primary key,
MaHD numeric(18,0) NOT NULL,
MaHang nvarchar(10) NOT NULL,
Soluong int NOT NULL

foreign key(MaHD) references dbo.tblHoaDon(MaHD),
foreign key(MaHang) references dbo.tblMatHang(MaHang)
)
INSERT INTO [dbo].[tblChiTietHD]([MaChiTietHD],[MaHD],[MaHang],[Soluong])VALUES(1, 1, N'M01', 2)
INSERT INTO [dbo].[tblChiTietHD]([MaChiTietHD],[MaHD],[MaHang],[Soluong])VALUES(2, 2, N'M02', 3)