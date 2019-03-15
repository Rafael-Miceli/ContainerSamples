CREATE DATABASE IF NOT EXISTS clientsdb;

use clientsdb;

CREATE TABLE client (
    id int auto_increment primary key, 
    firstname VARCHAR(200), 
    lastname VARCHAR(200)
);

insert into client (firstname, lastname) values
('Alex', 'Jr'),
('Ciclano', 'Silva'),
('Maria', 'Oliveira'),
('Jon', 'Doe'),
('Fulano', 'Costa');

commit;