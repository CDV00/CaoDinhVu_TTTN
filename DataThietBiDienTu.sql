--Categories
insert into Categories(Id,Name,Status,CreateAt,CreateBy,IsActive,IsDelete) values('228a4bcd-e90a-499b-8a92-15d07d1cc4fe',N'Điện thoại',1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Categories(Id,Name,Status,CreateAt,CreateBy,IsActive,IsDelete) values('228a4bcd-e90a-499b-8a92-25d07d1cc4fe',N'Máy tính xách tay',1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Categories(Id,Name,Status,CreateAt,CreateBy,IsActive,IsDelete) values('228a4bcd-e90a-499b-8a92-35d07d1cc4fe',N'Máy tính bảng',1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--Brands
insert into Brands(Id,Name,Status,CreateAt,CreateBy,IsActive,IsDelete) values('228a4bcd-e90a-499b-8a92-15d07d1cc4fe',N'Aplle',1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Brands(Id,Name,Status,CreateAt,CreateBy,IsActive,IsDelete) values('228a4bcd-e90a-499b-8a92-25d07d1cc4fe',N'Samsung',1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Brands(Id,Name,Status,CreateAt,CreateBy,IsActive,IsDelete) values('228a4bcd-e90a-499b-8a92-35d07d1cc4fe',N'Lenovo',1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--product
insert into Products(Id,Name,Title,CategoryId,BrandId,Price,Status,CreateAt,CreateBy,IsActive,IsDelete) values('128a4bcd-e90a-499b-8a92-45d07d1cc4fe',N'Iphone 12 pro',N'Iphone 12 pro','228a4bcd-e90a-499b-8a92-15d07d1cc4fe','228a4bcd-e90a-499b-8a92-15d07d1cc4fe',15000000,1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Products(Id,Name,Title,CategoryId,BrandId,Price,Status,CreateAt,CreateBy,IsActive,IsDelete) values('228a4bcd-e90a-499b-8a92-45d07d1cc4fe',N'Lenovo legion 5',N'Lenovo legion 5','228a4bcd-e90a-499b-8a92-25d07d1cc4fe','228a4bcd-e90a-499b-8a92-35d07d1cc4fe',15000000,1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Products(Id,Name,Title,CategoryId,BrandId,Price,Status,CreateAt,CreateBy,IsActive,IsDelete) values('328a4bcd-e90a-499b-8a92-45d07d1cc4fe',N'Samsung tab s7 pro',N'Samsung tab s7 pro','228a4bcd-e90a-499b-8a92-35d07d1cc4fe','228a4bcd-e90a-499b-8a92-25d07d1cc4fe',15000000,1,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--color
insert into Colors(Id,Name,Hex,CreateAt,CreateBy,IsActive,IsDelete) values('128a4bcd-e90a-499b-8a92-45d07d1cc4fe',N'Trắng',N'#FFFFFF','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Colors(Id,Name,Hex,CreateAt,CreateBy,IsActive,IsDelete) values('228a4bcd-e90a-499b-8a92-45d07d1cc4fe',N'Đen',N'#000000','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--ProductColors
--Iphone 12
insert into ProductColors(Id,ProductId,ColorId,CreateAt,CreateBy,IsActive,IsDelete)	values('128a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductColors(Id,ProductId,ColorId,CreateAt,CreateBy,IsActive,IsDelete)	values('228a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--lenovo legion 5
insert into ProductColors(Id,ProductId,ColorId,CreateAt,CreateBy,IsActive,IsDelete)	values('328a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductColors(Id,ProductId,ColorId,CreateAt,CreateBy,IsActive,IsDelete)	values('428a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--Samsung tab s7 pro
insert into ProductColors(Id,ProductId,ColorId,CreateAt,CreateBy,IsActive,IsDelete)	values('528a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductColors(Id,ProductId,ColorId,CreateAt,CreateBy,IsActive,IsDelete)	values('628a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--Options
insert into Options(Id,RAM,ROM,CreateAt,CreateBy,IsActive,IsDelete)	values('128a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,32,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Options(Id,RAM,ROM,CreateAt,CreateBy,IsActive,IsDelete)	values('228a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,64,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Options(Id,RAM,ROM,CreateAt,CreateBy,IsActive,IsDelete)	values('328a4bcd-e90a-499b-8a92-35d07d1cc4fe',8,256,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into Options(Id,RAM,ROM,CreateAt,CreateBy,IsActive,IsDelete)	values('428a4bcd-e90a-499b-8a92-35d07d1cc4fe',16,512,'09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--ProductOptions
--Iphone 12	trăng
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('128a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'128a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('228a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'128a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--Iphone 12	đen
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('328a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'228a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('428a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'228a4bcd-e90a-499b-8a92-45d07d1cc4fe','128a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--Lenovo Legion 5 trăng
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('528a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'328a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('628a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'328a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','428a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--Lenovo Legion 5 đen
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('728a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'428a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('828a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'428a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','428a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--Lenovo Legion 5 trăng
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('928a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'528a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('108a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'528a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
--Lenovo Legion 5 đen
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('118a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'628a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-45d07d1cc4fe','228a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
insert into ProductOptions(Id,Number,ProductColorId,ProductId,OptionId,CreateAt,CreateBy,IsActive,IsDelete)	values('138a4bcd-e90a-499b-8a92-35d07d1cc4fe',4,'628a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-45d07d1cc4fe','328a4bcd-e90a-499b-8a92-35d07d1cc4fe','09/29/2022','228a4bcd-e90a-499b-8a92-45d07d1cc4fe','True','False')
