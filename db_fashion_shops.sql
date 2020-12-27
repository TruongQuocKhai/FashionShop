
create database fashion_shop
go

use fashion_shop
go

create table [user](
	user_id int identity primary key,
	username varchar(50),
	display_name nvarchar(200),
	password varchar(32),
	address nvarchar(200),
	email varchar(50),
	phone varchar(50),
	created_date datetime default getdate(),
	created_by	nvarchar(250),
	edited_date datetime,
	edited_by nvarchar(250),
	status bit default 1
);
go

create table product_category(
	prd_category_id int identity primary key,
	prd_category_name nvarchar(250),
	alias varchar(250),
	parent_id int,
	display_order int default 0,
	created_date datetime default getdate(),
	created_by	nvarchar(250),
	edited_date datetime,
	edited_by nvarchar(250),
	status bit default 1
);
go

create table product(
	product_id int identity primary key,
	product_name nvarchar(250),
	prod_code varchar(20),
	alias varchar(250),
	description nvarchar(500),
	image varchar(250),
	sub_image xml,
	price decimal(18,0) default 0,
	discount decimal(18,0),
	quantity int not null default 0,
	detail ntext,
	created_date datetime default getdate(),
	created_by	nvarchar(250),
	edited_date datetime,
	edited_by nvarchar(250),
	top_hot datetime,
	status bit default 1,
	prd_category_id int
);
go

create table news_category(
	news_category_id int identity primary key,
	news_category_name nvarchar(250),
	alias varchar(250),
	parent_id int,
	display_order int default 0,
	created_date datetime,
	created_by	nvarchar(250),
	edited_date datetime,
	edited_by nvarchar(250),
	status bit default 1
);
go

create table content(
	content_id int identity primary key,
	content_name nvarchar(250),
	alias varchar(250),
	description nvarchar(500),
	image varchar(250),
	detail ntext,
	created_date datetime default getdate(),
	created_by	nvarchar(250),
	edited_date datetime,
	edited_by nvarchar(250),
	status bit default 1,
	news_category_id int,
	tag_id varchar(500)
);
go

create table tag(
	tag_id varchar(50) primary key,
	tag_name nvarchar(50)
);
go

create table content_tag(
	content_id int,
	tag_id varchar(50),
	
	constraint pk_content_tag primary key(content_id, tag_id) 
);
go

create table introduce(
	intro_id int identity primary key,
	intro_name nvarchar(250),
	alias varchar(250),
	image varchar(250),
	description nvarchar(500),
	detail ntext,
	created_date datetime default getdate(),
	created_by	nvarchar(250),
	edited_date datetime,
	edited_by nvarchar(250),
	status bit default 1,
);
go

create table contact(
	contact_id int identity primary key,
	content ntext,
	status bit default 1
);
go

create table feedback(
	feedback_id int identity primary key,
	feedback_name nvarchar(50),
	email varchar(50),
	subject varchar(250),
	message ntext
);
go

create table slide( 
	slide_id int identity primary key,
	image varchar(250),
	display_order int,
	link varchar(250),
	description nvarchar(100),
	created_date datetime default getdate(),
	created_by	nvarchar(250),
	edited_date datetime,
	edited_by nvarchar(250),
	status bit default 1,
);
go

create table menu_type(
	menu_type_id int identity primary key,
	name nvarchar(50)
);
go

create table menu(
	menu_id int identity primary key,
	text nvarchar(50),
	link varchar(250),
	display_order int,
	status bit default 1,
	menu_type_id int 
);
go

create table footer(
	footer_id varchar(50) primary key,
	content ntext,
	status bit default 1
);
go

create table [order](
	order_id int identity primary key,
	user_id int,
	order_name nvarchar(50),
	order_phone varchar(11),
	order_address nvarchar(250),
	order_email varchar(50),
	order_date datetime,
	status bit default 1
);
go

create table order_detail(
	product_id int, 
	order_id int,
	quanity int default 1,
	price decimal(18,0),
	constraint pk_order_detail primary key(product_id, order_id)
);






