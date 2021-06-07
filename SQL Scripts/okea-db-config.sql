use master;
GO
drop database if exists okea;
go
create database okea;
go
use okea;
go

create table Account (
AccountId int primary key identity(1,1),
FirstName varchar(50) not null,
LastName varchar(50) not null,
Email varchar(50) not null,
[Password] varchar(50) not null,
[Address] varchar(50) not null, 
DateOfBirth date not null,
IsAdmin bit not null 
);

create table Category (
CategoryId int primary key identity(1,1),
CategoryName varchar(25) not null 
);

create table PaymentMethod (
PaymentMethodId int primary key identity(1,1),
AccountId int not null,
CardHolderFirstName varchar(50) not null,
CardHolderLastName varchar(50) not null,
CardNumber varchar(16) not null,
CardExpiration  date not null,
CardCVV varchar(4) not null,
BillingAddress varchar(50) not null, 
constraint fk_Account_PaymentMethod_AccountId
	foreign key (AccountId)
	references Account(AccountId)
);

Create table Item (
	ItemId int primary key identity(1,1),
	ItemName varchar(50) not null,
	ItemDescription varchar(100) not null,
	UnitPrice decimal(12,2) not null,
	ImageName varchar(100) not null,
);

Create table CategoryItem (
	ItemId int not null,
	CategoryId int not null,
	Constraint fk_Item_CategoryItem_ItemId
		Foreign key (ItemId)
		References Item(ItemId),
	Constraint fk_Category_CategoryItem_CategoryId
		Foreign key (CategoryId)
		References Category(CategoryId),
	Constraint pk_CategoryItem
		Primary key (ItemId, CategoryId)
);

Create table Cart (
	CartId int primary key identity(1,1),
	AccountId int not null,
	PaymentMethodId int null,
	OrderTotal decimal(12,2) not null,
	CheckOutDate datetime null,
	Constraint fk_Account_Cart_AccountId
		Foreign key (AccountId)
		References Account(AccountId),
	Constraint fk_PaymentMethod_Cart_PaymentMethodId
		Foreign key (PaymentMethodId)
		References PaymentMethod(PaymentMethodId)
);

Create table CartItem (
    CartId int not null,
    ItemId int not null,
    Quantity int not null,
    Constraint fk_Cart_CartItem_CartId
        foreign key (CartId)
        references Cart(CartId),
    Constraint fk_Item_CartItem_ItemId
        foreign key (ItemId)
        references Item(ItemId),
    Constraint pk_CartItem
        primary key (CartId, ItemId)
);

/* Mockeroo populating */

insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Joey', 'Nequest', 'jnequest0@angelfire.com', 'rMVk6R', '93 Pearson Center', '2004-07-07 14:01:26', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Dani', 'Fearnehough', 'dfearnehough1@exblog.jp', '4qPLlW', '32 Forest Dale Hill', '1969-01-13 01:26:57', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Chadd', 'Wooding', 'cwooding2@blog.com', 'd2fYtHLA', '5 Grim Crossing', '1986-06-15 03:41:53', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Rodger', 'Petegree', 'rpetegree3@mysql.com', 'fuTYNw8Ti', '95905 Dennis Hill', '1960-06-02 00:04:32', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Olivia', 'Hamshaw', 'ohamshaw4@slate.com', 'GbjfFkwfrw', '2 Melody Road', '2009-10-05 02:31:08', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Aimee', 'McGinley', 'amcginley5@goodreads.com', 'tiki3Qef1', '26 Center Alley', '1961-04-21 08:37:10', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Stanleigh', 'Yair', 'syair6@ucoz.ru', 'Ck2Z9eQW2', '18142 Briar Crest Point', '1988-04-30 14:43:32', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Hynda', 'Longson', 'hlongson7@topsy.com', 'YNqVqjUbD21y', '9422 School Park', '1989-10-10 04:20:30', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Liane', 'Willmetts', 'lwillmetts8@ocn.ne.jp', 'ks2UX5Be', '62 Grim Circle', '1989-01-06 08:52:39', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Eldredge', 'Fulloway', 'efulloway9@last.fm', 'PsEzmtIDMOI', '13 Moulton Court', '1997-11-15 11:33:29', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Tarah', 'Please', 'tpleasea@paginegialle.it', 'Nd96VoAdDE', '892 Dryden Court', '1987-11-16 12:59:38', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Victoria', 'Heersma', 'vheersmab@ft.com', '4hX49POC96', '00 Mockingbird Point', '1994-01-11 20:18:23', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Rodger', 'Kleinbaum', 'rkleinbaumc@shutterfly.com', 'Dn0ZXl', '2546 Packers Junction', '1975-04-21 20:47:58', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Larina', 'Shefton', 'lsheftond@discuz.net', 'hKCjJKyMjRO', '4425 Carioca Court', '2003-05-03 13:19:29', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Regen', 'Orridge', 'rorridgee@csmonitor.com', 'In2oVW', '3 Pleasure Avenue', '1977-02-05 02:58:43', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Stacie', 'Filippyev', 'sfilippyevf@live.com', '3FCz9V', '97 Sauthoff Avenue', '1977-04-06 23:38:24', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Skylar', 'Munt', 'smuntg@rambler.ru', 'jr6qkQzkDFT', '75 Marcy Avenue', '1968-09-12 02:06:09', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Alfred', 'Gregg', 'agreggh@economist.com', '24UbKEcfG', '196 High Crossing Court', '1985-12-19 00:44:50', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Evanne', 'Scrinage', 'escrinagei@lulu.com', 'E8to4lG2Dx', '227 Dapin Court', '1962-11-27 22:04:10', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Sadie', 'Loud', 'sloudj@paypal.com', 'YXgEw4', '0 Ridge Oak Hill', '1964-12-04 06:17:44', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Field', 'Pettifer', 'fpettiferk@cmu.edu', 'EmhES54zLyPf', '4376 Sommers Circle', '1978-02-04 18:56:06', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Brant', 'Fallon', 'bfallonl@printfriendly.com', 'OmsxsM4', '10 Oak Valley Way', '1963-09-16 13:27:08', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Elsbeth', 'Basketfield', 'ebasketfieldm@who.int', 'mpW53TKpu1', '36151 Reinke Junction', '2012-12-30 05:36:18', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Madge', 'Dafter', 'mdaftern@eventbrite.com', 'FgH1TcOGnl7e', '7252 6th Alley', '2002-11-30 22:18:11', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Merla', 'Panons', 'mpanonso@qq.com', 'cczHebNqjl9', '37 Warbler Parkway', '1994-06-23 15:22:00', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Stephana', 'Ellingford', 'sellingfordp@nba.com', '6C562YdWm', '445 Marcy Way', '1992-11-21 11:18:04', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Gayla', 'Canete', 'gcaneteq@trellian.com', '68NLJ7', '27 Meadow Vale Center', '1968-07-14 04:35:53', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Radcliffe', 'Rolinson', 'rrolinsonr@icq.com', 'pd1OYbMLTY', '132 Nelson Street', '1963-09-25 04:32:19', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Guillemette', 'Brise', 'gbrises@discuz.net', 'IBxre9QFK', '59646 Prentice Trail', '2018-12-03 11:53:24', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Nickola', 'Lordon', 'nlordont@free.fr', 'onrBy8Ze4ePk', '1964 Sugar Hill', '1967-10-28 21:43:47', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Claresta', 'Ben', 'cbenu@shop-pro.jp', 'cEBXm5', '0 3rd Hill', '2005-10-07 13:14:36', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Otha', 'Girardengo', 'ogirardengov@gravatar.com', 'GeDUxLUlEKQO', '78965 Utah Junction', '2019-03-28 07:26:08', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Em', 'Snawdon', 'esnawdonw@dot.gov', 'UEXiLEFuD', '1486 Sunbrook Junction', '1997-02-23 01:19:39', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Chrystal', 'Mayling', 'cmaylingx@eventbrite.com', 'hGHLROLhW5', '29030 Atwood Trail', '2006-10-25 22:33:20', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Gaelan', 'MacHarg', 'gmachargy@networksolutions.com', 'vueUo8tk', '0627 Eagle Crest Street', '1984-01-26 13:40:28', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Talyah', 'Cohan', 'tcohanz@etsy.com', 'gfRBQZcc', '4403 Thackeray Drive', '1990-01-15 15:41:01', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Sharla', 'Causby', 'scausby10@desdev.cn', 'Be6iXdMR1Et', '02465 Towne Alley', '1989-12-21 12:09:02', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Sula', 'Eastwood', 'seastwood11@redcross.org', 'lYBPzq7B', '494 Dayton Center', '1978-07-06 13:57:14', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Erich', 'Daltrey', 'edaltrey12@cornell.edu', 'njJp9Dp4', '619 Sugar Crossing', '1989-01-27 11:19:55', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Boycie', 'Whitney', 'bwhitney13@netvibes.com', 'VDnrwLPXfUF', '758 Crest Line Parkway', '1961-10-21 06:41:06', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Sissy', 'Blest', 'sblest14@studiopress.com', 'LLJuD5El', '29 Upham Way', '1968-11-20 09:55:47', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Evie', 'Ianitti', 'eianitti15@nyu.edu', 'sp2mIWWGb', '51 Jenna Center', '1973-03-06 06:29:54', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Dar', 'Cosley', 'dcosley16@eventbrite.com', 'XEYvkM4R', '10360 Acker Place', '1995-07-22 00:55:01', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Anatollo', 'Bevington', 'abevington17@bizjournals.com', 'YZhQ0XLiY', '678 Weeping Birch Drive', '1990-05-16 18:03:48', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Kaile', 'Girhard', 'kgirhard18@unesco.org', 'dFKj1i4vzz', '68 Clarendon Court', '1983-11-26 09:25:35', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Axel', 'MacAvaddy', 'amacavaddy19@friendfeed.com', 'GhHQB3', '7236 Hanover Court', '2015-12-29 05:54:03', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Polly', 'Benbow', 'pbenbow1a@booking.com', 'gbWszUI66Or', '50138 High Crossing Lane', '2008-10-20 20:59:04', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Wilburt', 'Sweetland', 'wsweetland1b@geocities.jp', '1rZ0CZdh', '439 John Wall Way', '1965-12-12 01:04:21', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Bonny', 'Kayser', 'bkayser1c@about.me', 'GHplW6', '9650 Summit Circle', '2012-02-14 20:03:32', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Griswold', 'Hudleston', 'ghudleston1d@twitpic.com', 'DcyvIJbs0aT', '60 Meadow Vale Alley', '1995-05-24 08:39:47', 0);
insert into Account (FirstName, LastName, Email, Password, Address, DateOfBirth, IsAdmin) values ('Admin', 'Account', 'admin@dev-10.com', 'admin123', 'Address', '2021-01-01 01:01:01', 1);

set identity_insert Category on;
insert into Category (CategoryId, CategoryName) values (1, 'Sofa');
insert into Category (CategoryId, CategoryName) values (2, 'Sectional');
insert into Category (CategoryId, CategoryName) values (3, 'Chair');
insert into Category (CategoryId, CategoryName) values (4, 'Loveseat');
insert into Category (CategoryId, CategoryName) values (5, 'Recliner');
insert into Category (CategoryId, CategoryName) values (6, 'Coffee table');
insert into Category (CategoryId, CategoryName) values (7, 'Bench');
insert into Category (CategoryId, CategoryName) values (8, 'Table');
insert into Category (CategoryId, CategoryName) values (9, 'Bookcase');
insert into Category (CategoryId, CategoryName) values (10, 'Bed');
insert into Category (CategoryId, CategoryName) values (11, 'End Table');
insert into Category (CategoryId, CategoryName) values (12, 'Mirror');
insert into Category (CategoryId, CategoryName) values (13, 'Desk');
insert into Category (CategoryId, CategoryName) values (14, 'Ottoman');
insert into Category (CategoryId, CategoryName) values (15, 'Dresser');
set identity_insert Category off;

insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (35, 'Angel', 'McCorry', 1363201241458749, '09/18/2018', 1060, '78 Moose Hill');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (10, 'Erl', 'Jancey', 6598442914148203, '07/08/2019', 6935, '03956 Carberry Crossing');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (39, 'Waring', 'Carse', 5874780748273248, '01/17/2022', 3176, '3 Gerald Avenue');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (16, 'Gill', 'Millward', 5334117565482455, '02/26/2018', 7710, '6 Thackeray Park');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (26, 'Hadrian', 'Hindshaw', 7452091405971987, '08/22/2022', 4735, '5 Loomis Trail');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (20, 'Elicia', 'Crees', 4693520532971771, '12/05/2022', 5535, '50 Troy Drive');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (16, 'Hermie', 'Aldred', 4765392900068862, '06/13/2023', 1950, '53 Meadow Valley Road');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (32, 'Davey', 'Paulillo', 3681899350341346, '05/31/2018', 5837, '840 Sauthoff Parkway');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (15, 'Noreen', 'Hanney', 4378295892195674, '12/31/2023', 4485, '4 Brickson Park Place');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (15, 'Gabbey', 'Teal', 3143884324176137, '04/02/2018', 5618, '48 Dovetail Way');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (19, 'Germaine', 'Rowe', 3249668764420493, '01/10/2023', 6785, '136 Hanover Way');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (27, 'Marven', 'Rosengarten', 1682665178665519, '12/05/2018', 1892, '57013 Green Circle');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (50, 'Royal', 'Viel', 3133152094282318, '08/24/2020', 5060, '7462 Mifflin Place');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (50, 'Salvador', 'Northrop', 2221776759818825, '09/10/2021', 8824, '5 Briar Crest Plaza');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (4, 'Hildagarde', 'Legate', 9024527984706569, '12/19/2018', 4369, '172 Nancy Junction');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (4, 'Jaime', 'Markovic', 7330476898790574, '07/09/2019', 6559, '34 Sage Lane');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (29, 'Guenna', 'McAndie', 4328045212053446, '09/19/2018', 3150, '56149 Golf Parkway');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (9, 'Caldwell', 'Amorts', 2899151229950603, '12/30/2018', 4756, '014 Shoshone Crossing');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (5, 'Ira', 'Greatrex', 7631576806319221, '07/31/2020', 5338, '14563 Kim Hill');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (20, 'Garrick', 'Schistl', 4230384953581294, '06/01/2021', 1795, '701 Oneill Drive');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (2, 'Corbin', 'Keam', 7012308717997607, '12/19/2019', 9207, '0 4th Lane');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (38, 'Sol', 'Loncaster', 6596203061245671, '04/07/2023', 5531, '2 Oak Valley Trail');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (45, 'Fayth', 'Sharrard', 3682606472012710, '07/01/2019', 5356, '6925 Lake View Circle');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (29, 'Georgine', 'Potts', 9629273033745305, '09/26/2023', 8251, '399 Sherman Parkway');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (31, 'Amalee', 'Hards', 5019141540820894, '09/18/2019', 3375, '8376 Sutherland Road');
insert into PaymentMethod (AccountId, CardHolderFirstName, CardHolderLastName, CardNumber, CardExpiration, CardCVV, BillingAddress) values (1, 'Joey', 'Nequest', 9089141540820894, '04/18/2022', 9875, '201 South Wabash Avenue');

set identity_insert Item on;
insert into Item (ItemId, ItemName, ItemDescription, UnitPrice, ImageName) values (1, 'Uppland', 'Sectional, 4-seat corner', 799.00, 'Uppland.png');
insert into Item (ItemId, ItemName, ItemDescription, UnitPrice, ImageName) values (2, 'Neiden', 'Bed frame, twin', 59.00, 'Neiden.png');
insert into Item (ItemId, ItemName, ItemDescription, UnitPrice. ImageName) values (3, 'Hemnes', '8-drawer dresser, 63x37 3/8 "', 249.00, 'Hemnes.png');
insert into Item (ItemId, ItemName, ItemDescription, UnitPrice, ImageName) values (4, 'Lerhamn', 'Table, 29 1/8x29 1/8 "', 69.00, 'Lerhamn.png');
set identity_insert Item off;

insert into CategoryItem (ItemId, CategoryId) values (1,2);
insert into CategoryItem (ItemId, CategoryId) values (2,10);
insert into CategoryItem (ItemId, CategoryId) values (3,15);
insert into CategoryItem (ItemId, CategoryId) values (4,8);

insert into Cart 
(AccountId, PaymentMethodId, OrderTotal, CheckedOut) 
values 
(1, null, 1245.0, null),
(1, 26, 187.0, '2021-06-06T12:15');

insert into CartItem 
(CartId, ItemId, Quantity) 
values 
(1, 1, 1),
(1, 2, 1),
(1, 3, 1),
(1, 4, 2),
(2, 2, 2),
(2, 4, 1);