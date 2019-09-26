USE master
DROP DATABASE IF EXISTS db
CREATE DATABASE db
USE db
CREATE TABLE accounts(
id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
username varchar(24) NOT NULL,
password varchar(60) NOT NULL,
rank int NOT NULL DEFAULT 0,
banned bit NOT NULL DEFAULT 'False',
)
INSERT INTO accounts (username, password, rank) VALUES ('Admin', 'root', '2')
CREATE TABLE no_encrypt(
username varchar(24) NOT NULL,
)
INSERT INTO no_encrypt (username) VALUES ('Admin')
CREATE TABLE settings(
theme bit NOT NULL DEFAULT 0,
)
INSERT INTO settings (theme) VALUES ('False')