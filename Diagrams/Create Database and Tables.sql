-- CREATE DATABASE electrifynz;

DROP TABLE IF EXISTS itemorder;
DROP TABLE IF EXISTS item;
DROP TABLE IF EXISTS category;

CREATE TABLE category (
	name VARCHAR(40) NOT NULL UNIQUE,
    description VARCHAR (256) NOT NULL,
    PRIMARY KEY (name)
); 

CREATE TABLE item (
	id INT NOT NULL UNIQUE,
    categoryName VARCHAR(40) NOT NULL,
    image IMAGE,
    name VARCHAR(30) NOT NULL,
    description VARCHAR(256) NOT NULL,
    price DECIMAL(8,2) NOT NULL,
    modifiedDate DATE NOT NULL,
    quantity TINYINT NOT NULL,
    motor VARCHAR(40) NOT NULL,
    warrantyPeriod TINYINT,
    condition VARCHAR(10),
    PRIMARY KEY (id),
    FOREIGN KEY (categoryName) REFERENCES category(name)
);

CREATE TABLE itemorder (
	invoiceNumber INT NOT NULL UNIQUE,
    itemID INT NOT NULL,
    quantity TINYINT NOT NULL,
    price DECIMAL(8,2) NOT NULL,
    name VARCHAR(30) NOT NULL,
    email VARCHAR(50) NOT NULL,
    PRIMARY KEY (invoiceNumber),
    FOREIGN KEY (itemID) REFERENCES item(id)
);