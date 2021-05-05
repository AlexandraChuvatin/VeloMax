DROP DATABASE IF EXISTS VeloMax;
CREATE DATABASE IF NOT EXISTS VeloMax;
USE VeloMax;

#Création des tables
CREATE TABLE modele (num_modele varchar(40) NOT NULL, prix_modele float, ligne_produit varchar(40), nom_modele varchar(40), grandeur varchar(40), date_intro_modele datetime, date_disc_modele datetime, cadre varchar(40), guidon varchar(40), freins varchar(40), selle varchar(40), derailleur_avant varchar(40), derailleur_arriere varchar(40), roue_avant varchar(40), roue_arriere varchar(40), reflecteurs varchar(40), pedalier varchar(40), ordinateur varchar(40), panier varchar(40), stock_modele int);
CREATE TABLE piece (num_piece varchar(40) NOT NULL, description varchar(40), nom_fournisseur varchar(40), num_produit_catalogue varchar(40), prix_piece float, date_intro_piece datetime, date_disc_piece datetime, delai varchar(40), stock_piece int);
CREATE TABLE individu (nom_individu varchar(40) NOT NULL, prenom_individu varchar(40), adresse_individu varchar(40), tel_individu varchar(40), mail_individu varchar(40), date_adhesion datetime, num_programme int);
CREATE TABLE commande (num_commande int NOT NULL, date_commande datetime, adresse_livraison varchar(40), date_livraison datetime, nom_individu varchar(40), nom_boutique varchar(40));
CREATE TABLE boutique (nom_boutique varchar(40) NOT NULL, adresse_boutique varchar(40), tel_boutique varchar(40), mail_boutique varchar(40), nom_contact varchar(40), remise float);
CREATE TABLE fournisseur (siret varchar(40) NOT NULL, nom_entreprise varchar(40), contact_fournisseur varchar(40), adresse_fournisseur varchar(40), libelle int);
CREATE TABLE fidelio (num_programme int NOT NULL, description_programme varchar(40), cout_programme float, duree_programme varchar(40), rabais_programme float);
CREATE TABLE contient_modele (num_commande int NOT NULL, num_modele varchar(40) NOT NULL, quantite_modele int);
CREATE TABLE approvision (num_piece varchar(40) NOT NULL, siret varchar(40) NOT NULL);
CREATE TABLE contient_piece (num_commande int NOT NULL, num_piece varchar(40) NOT NULL, quantite_piece int);

#Création des contraintes
ALTER TABLE modele ADD CONSTRAINT PK_modele PRIMARY KEY (num_modele);
ALTER TABLE piece ADD CONSTRAINT PK_piece PRIMARY KEY (num_piece);
ALTER TABLE individu ADD CONSTRAINT PK_individu PRIMARY KEY (nom_individu);
ALTER TABLE commande ADD CONSTRAINT PK_commande PRIMARY KEY (num_commande);
ALTER TABLE boutique ADD CONSTRAINT PK_boutique PRIMARY KEY (nom_boutique);
ALTER TABLE fournisseur ADD CONSTRAINT PK_fournisseur PRIMARY KEY (siret);
ALTER TABLE fidelio ADD CONSTRAINT PK_fidelio PRIMARY KEY (num_programme);
ALTER TABLE contient_modele ADD CONSTRAINT PK_contient_modele PRIMARY KEY (num_commande, num_modele);
ALTER TABLE approvision ADD CONSTRAINT PK_approvision PRIMARY KEY (num_piece, siret);
ALTER TABLE contient_piece ADD CONSTRAINT PK_contient_piece PRIMARY KEY (num_commande, num_piece);
ALTER TABLE individu ADD CONSTRAINT FK_individu_num_programme FOREIGN KEY (num_programme) REFERENCES fidelio (num_programme);
ALTER TABLE commande ADD CONSTRAINT FK_commande_nom_individu FOREIGN KEY (nom_individu) REFERENCES individu (nom_individu);
ALTER TABLE commande ADD CONSTRAINT FK_commande_nom_boutique FOREIGN KEY (nom_boutique) REFERENCES boutique (nom_boutique);
ALTER TABLE contient_modele ADD CONSTRAINT FK_contient_modele_num_commande FOREIGN KEY (num_commande) REFERENCES commande (num_commande);
ALTER TABLE contient_modele ADD CONSTRAINT FK_contient_modele_num_modele FOREIGN KEY (num_modele) REFERENCES modele (num_modele);
ALTER TABLE approvision ADD CONSTRAINT FK_approvision_num_piece FOREIGN KEY (num_piece) REFERENCES piece (num_piece);
ALTER TABLE approvision ADD CONSTRAINT FK_approvision_siret FOREIGN KEY (siret) REFERENCES fournisseur (siret);
ALTER TABLE contient_piece ADD CONSTRAINT FK_contient_piece_num_commande FOREIGN KEY (num_commande) REFERENCES commande (num_commande);
ALTER TABLE contient_piece ADD CONSTRAINT FK_contient_piece_num_piece FOREIGN KEY (num_piece) REFERENCES piece (num_piece);

# Insertion de données

# fournisseur
insert into fournisseur VALUES ('2901099490', 'CycloVelo', 'Michel Rampe', '11 rue de la Paix', 1);

# piece
insert into piece VALUES ('C30', 'cadre', 'CycloVelo', 'C30', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('G7', 'guidon', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('F3', 'freins', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('S88', 'selle', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('DV133', 'dérailleur avant', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('DR188', 'dérailleur arrière', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R46', 'roue', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R57', 'roue', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R02', 'réflecteurs', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('P1', 'pédalier', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('O2', 'odrinateur', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('S01', 'panier', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);

# approvision 
insert into approvision VALUES ('C30', '2901099490');

# modele
insert into modele VALUES ('VTT3000', 569, 'VTT', 'Kilimandjaro', 'Adultes', '2021-01-01', '2022-01-01', 'C30', 'G7', 'F3', 'S88', 'DV133', 'DR188', 'R46', 'R57', 'R02', 'P1', 'O2', 'S01', 2);

# fidelio
insert into fidelio VALUES (1, 'Fidélio', 15, '1 an', 0.05);
insert into fidelio VALUES (2, 'Fidélio Or', 25, '2 ans', 0.08);
insert into fidelio VALUES (3, 'Fidélio Platine', 60, '2 ans', 0.10);
insert into fidelio VALUES (4, 'Fidélio Max', 100, '3 ans', 0.12);

# boutique
insert into boutique VALUES ('CycloSurf', '11 rue des lilas, Paris, 75', '0667766717', 'cyclosurf@gmail.com', 'Jean Paire', 0.10);

# individu
insert into individu VALUES ('Alaphilippe','Julian','11 rue des Gascons, Paris, 75', '0781391891', 'jean@gmail.com', '2021-01-01', 1);
insert into individu VALUES ('Ladagnous','Matthieu','11 rue des Gascons, Paris, 75', '0781391891', 'jean@gmail.com', '2021-01-01', 2);

# commande
insert into commande VALUES (2, '2021-04-25','26 rue des noyers, Paris, 75', '2021-05-28', 'Ladagnous', null);

# contient_modele
insert into contient_modele VALUES (2, 'VTT3000', 3);

# contient_piece
insert into contient_piece VALUES (2, 'C30', 1);












