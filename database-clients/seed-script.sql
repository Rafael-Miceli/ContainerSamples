CREATE DATABASE IF NOT EXISTS clientsdb;

use clientsdb;

CREATE TABLE client (
    id int auto_increment primary key, 
    firstname VARCHAR(200), 
    lastname VARCHAR(200)
);

insert into client values
(1, 'Alex', 'Jr'),
(2, 'Ciclano', 'Silva'),
(3, 'Maria', 'Oliveira'),
(4, 'Jon', 'Doe'),
(5, 'Fulano', 'Costa')