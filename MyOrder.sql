--create database MyOrder
create table dbo.tblUser (
Username varchar(50),
Pass int
)
INSERT INTO [dbo].[tblUser]([Username],[Pass])VALUES('duong',1)
INSERT INTO [dbo].[tblUser]([Username],[Pass])VALUES('duyen',2)
INSERT INTO [dbo].[tblUser]([Username],[Pass])VALUES('huy',3)
INSERT INTO [dbo].[tblUser]([Username],[Pass])VALUES('hung',4)
create table dbo.tblKhachHang (
MaKH varchar(10) primary key,
TenKH nvarchar(50),
GT bit,
Diachi nvarchar(50),
NgaySinh smalldatetime
)
INSERT INTO [dbo].[tblKhachHang]([MaKH],[TenKH],[GT],[Diachi],[NgaySinh])VALUES('KH01','Duong','True','Phù Yên',29/06/2022)
INSERT INTO [dbo].[tblKhachHang]([MaKH],[TenKH],[GT],[Diachi],[NgaySinh])VALUES('KH02','Huy','True','Phù Yên',30/06/2022)
create table dbo.tblHoaDon (
MaHD numeric(18,0) primary key,
MaKH varchar(10),
NgayHD smalldatetime

foreign key(MaKH) references dbo.tblKhachHang(MaKH)
)
INSERT INTO [dbo].[tblHoaDon]([MaHD],[MaKH],[NgayHD])VALUES(1, 'KH01', 29/06/2022)
INSERT INTO [dbo].[tblHoaDon]([MaHD],[MaKH],[NgayHD])VALUES(2, 'KH02', 30/06/2022)
create table dbo.tblDonViTinh (
DVT varchar(50) primary key
)
INSERT INTO [dbo].[tblDonViTinh]([DVT])VALUES('Chiếc')
INSERT INTO [dbo].[tblDonViTinh]([DVT])VALUES('Con')
INSERT INTO [dbo].[tblDonViTinh]([DVT])VALUES('Cái')
INSERT INTO [dbo].[tblDonViTinh]([DVT])VALUES('Thanh')
create table dbo.tblMatHang (
MaHang varchar(10) primary key,
TenHang nvarchar(50),
DVT varchar(50),
Gia real

foreign key(DVT) references dbo.tblDonViTinh(DVT)
)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia])VALUES('K01', 'Bàn phím', 'Chiếc', 200000)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia])VALUES('K02', 'Bàn phím ảo', 'Chiếc', 135000)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia])VALUES('M01', 'Chuột quang', 'Con', 82000)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia])VALUES('M02', 'Màn hình hỏng', 'Cái', 10000)
INSERT INTO [dbo].[tblMatHang]([MaHang],[TenHang],[DVT],[Gia])VALUES('R01', 'Ram Kington 4GB', 'Thanh', 17000)
create table dbo.tblChiTietHD (
MaChiTietHD numeric(18,0) primary key,
MaHD numeric(18,0),
MaHang varchar(10),
Soluong int

foreign key(MaHD) references dbo.tblHoaDon(MaHD),
foreign key(MaHang) references dbo.tblMatHang(MaHang)
)
INSERT INTO [dbo].[tblChiTietHD]([MaChiTietHD],[MaHD],[MaHang],[Soluong])VALUES(1, 1, 'M01', 2)
INSERT INTO [dbo].[tblChiTietHD]([MaChiTietHD],[MaHD],[MaHang],[Soluong])VALUES(2, 2, 'M02', 3)