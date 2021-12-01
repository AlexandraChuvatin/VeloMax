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
insert into fournisseur VALUES ('2901099490', 'CycloVelo', 'Michel Rampe', '11 rue de la Paix 75012 Paris', 1);
insert into fournisseur VALUES ('3101129842', 'RouloMax', 'Jean Castagne', '5 avenue de la République 75004 Paris', 2);
insert into fournisseur VALUES ('5937403140', 'EnRoueLibre', 'Claire Deneuve', '6bis chemin des bois 97403 Clamart', 1);

# piece
insert into piece VALUES ('C30', 'cadre', 'CycloVelo', 'C30', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('G7', 'guidon', 'CycloVelo', 'G7', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('F3', 'freins', 'CycloVelo', 'F3', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('S88', 'selle', 'CycloVelo', 'S88', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('DV133', 'dérailleur avant', 'CycloVelo', 'DV133', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('DR188', 'dérailleur arrière', 'CycloVelo', 'DR188', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R46', 'roue', 'CycloVelo', 'R46', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R57', 'roue', 'CycloVelo', 'R57', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R02', 'réflecteurs', 'CycloVelo', 'R02', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('P1', 'pédalier', 'CycloVelo', 'P1', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('O2', 'odrinateur', 'CycloVelo', 'O2', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('S01', 'panier', 'CycloVelo', 'S01', 5, '2021-01-01', '2022-01-01', '1 mois', 2);

insert into piece VALUES ('C31', 'cadre', 'RouloMax', 'C31', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('G8', 'guidon', 'RouloMax', 'G8', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('F4', 'freins', 'RouloMax', 'F4', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('S89', 'selle', 'RouloMax', 'S89', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('DV134', 'dérailleur avant', 'RouloMax', 'DV134', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('DR189', 'dérailleur arrière', 'RouloMax', 'DR189', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R47', 'roue', 'RouloMax', 'R47', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R58', 'roue', 'RouloMax', 'R58', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R03', 'réflecteurs', 'RouloMax', 'R03', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('P2', 'pédalier', 'RouloMax', 'P2', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('O3', 'odrinateur', 'RouloMax', 'O3', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('S02', 'panier', 'RouloMax', 'S02', 5, '2021-01-01', '2022-01-01', '1 mois', 2);

insert into piece VALUES ('C32', 'cadre', 'EnRoueLibre', 'C32', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('G9', 'guidon', 'EnRoueLibre', 'G9', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('F5', 'freins', 'EnRoueLibre', 'F5', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('S90', 'selle', 'EnRoueLibre', 'S90', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('DV135', 'dérailleur avant', 'EnRoueLibre', 'DV135', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('DR190', 'dérailleur arrière', 'EnRoueLibre', 'DR190', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R48', 'roue', 'EnRoueLibre', 'R48', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R59', 'roue', 'EnRoueLibre', 'R59', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('R04', 'réflecteurs', 'EnRoueLibre', 'R04', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('P3', 'pédalier', 'EnRoueLibre', 'P3', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('O4', 'odrinateur', 'EnRoueLibre', 'O4', 5, '2021-01-01', '2022-01-01', '1 mois', 2);
insert into piece VALUES ('S03', 'panier', 'EnRoueLibre', 'S03', 5, '2021-01-01', '2022-01-01', '1 mois', 2);

# approvision 
insert into approvision VALUES ('C30', '2901099490');
insert into approvision VALUES ('C31', '3101129842');
insert into approvision VALUES ('C32', '5937403140');
insert into approvision VALUES ('S01', '2901099490');
insert into approvision VALUES ('S02', '3101129842');
insert into approvision VALUES ('S03', '5937403140');
insert into approvision VALUES ('DV133', '2901099490');
insert into approvision VALUES ('DV134', '3101129842');
insert into approvision VALUES ('DV135', '5937403140');

# modele
insert into modele VALUES ('VTT3000', 569, 'VTT', 'Kilimandjaro', 'Adultes', '2021-01-01', '2022-01-01', 'C30', 'G7', 'F3', 'S88', 'DV133', 'DR188', 'R46', 'R57', 'R02', 'P3', 'O2', null, 2);
insert into modele VALUES ('VTT666', 999, 'VTT', 'Hooligan', 'Jeunes', '2018-01-01', '2023-01-01', 'C31', 'G8', 'F4', 'S89', 'DV134', 'DR189', 'R47', 'R58', 'R03', 'P2', null, null, 4);
insert into modele VALUES ('BMX123', 512, 'BMX', 'Mud Zinger I', 'Jeunes', '2020-01-01', '2023-01-01', 'C32', 'G7', 'F3', 'S90', 'DV135', 'DR188', 'R47', 'R59', 'R02', 'P1', null, null, 2);
insert into modele VALUES ('BMX456', 400, 'BMX', 'Mud Zinger II', 'Adultes', '2020-02-03', '2024-01-01', 'C30', 'G7', 'F3', 'S88', 'DV133', 'DR188', 'R46', 'R57', 'R02', 'P2', 'O2', null, 10);
insert into modele VALUES ('CL98', 199, 'Classique', 'Tierra Verde', 'Dames', '2020-02-04', '2024-02-05', 'C32', 'G8', 'F3', 'S88', 'DV134', 'DR188', 'R46', 'R57', 'R02', 'P3', 'O3', 'S02', 2);


# fidelio
insert into fidelio VALUES (1, 'Fidélio', 15, '1 an', 0.05);
insert into fidelio VALUES (2, 'Fidélio Or', 25, '2 ans', 0.08);
insert into fidelio VALUES (3, 'Fidélio Platine', 60, '2 ans', 0.10);
insert into fidelio VALUES (4, 'Fidélio Max', 100, '3 ans', 0.12);

# boutique
insert into boutique VALUES ('CycloSurf', '11 rue des lilas, Paris, 75', '0667766717', 'cyclosurf@gmail.com', 'Jean Paire', 0.10);
insert into boutique VALUES ('HyperVitesse', '5 rue des coquelicots, Paris, 75', '0694796729', 'hypervitesse@gmail.com', 'Luc Bessont', 0.25);
insert into boutique VALUES ('VeloKing', '42 chemin du tonneau, Paris, 75', '0693685109', 'veloking@gmail.com', 'Jean Rounot', 0.15);

# individu
insert into individu VALUES ('Alaphilippe','Julian','11 rue des Gascons, Paris, 75', '0781391891', 'julian@gmail.com', '2021-01-01', 1);
insert into individu VALUES ('Ladagnous','Matthieu','4 avenue du Paprika, Paris, 75', '0643950277', 'matthieu@gmail.com', '2019-02-06', 2);
insert into individu VALUES ('Dufour','Clothilde','5ter rue Lavoisier, Paris, 75', '0789537022', 'clothilde@gmail.com', '2021-01-01', 1);
insert into individu VALUES ('Delagalette','François','11 allée des champs, Paris, 75', '0756070093', 'francois@gmail.com', null, null);
insert into individu VALUES ('Lagarce','Maxime','2 avenue du Général Leclerc, Paris, 75', '0665773190', 'maxime@gmail.com', '2020-02-11', 3);
insert into individu VALUES ('Martinot','Pierre','6 allée des moissons, Paris, 75', '0753910833', 'pierre@gmail.com', null, null);

# commande
insert into commande VALUES (1, '2021-02-25','26 rue des noyers, Paris, 75', '2021-05-28', 'Ladagnous', null);
insert into commande VALUES (2, '2021-02-27','2 avenue du Général Leclerc, Paris, 75', '2021-05-28', 'Lagarce', null);
insert into commande VALUES (3, '2021-03-02','6 allée des moissons, Paris, 75', '2021-05-28', 'Martinot', null);
insert into commande VALUES (4, '2021-03-03','11 rue des lilas, Paris, 75', '2021-05-28', null, 'CycloSurf');
insert into commande VALUES (5, '2021-03-12','42 chemin du tonneau, Paris, 75', '2021-05-28', null, 'VeloKing');
insert into commande VALUES (6, '2021-04-05','26 rue des noyers, Paris, 75', '2021-05-28', 'Ladagnous', null);
insert into commande VALUES (7, '2021-04-11','42 chemin du tonneau, Paris, 75', '2021-05-28', null, 'VeloKing');
insert into commande VALUES (8, '2021-04-22','11 rue des Gascons, Paris, 75', '2021-05-28', 'Alaphilippe', null);
insert into commande VALUES (9, '2021-05-02','11 allée des champs, Paris, 75', '2021-05-28', 'Delagalette', null);
insert into commande VALUES (10, '2021-05-11','5 rue des coquelicots, Paris, 75', '2021-05-28', null, 'HyperVitesse');

# contient_modele
insert into contient_modele VALUES (2, 'VTT3000', 1);
insert into contient_modele VALUES (3, 'CL98', 1);
insert into contient_modele VALUES (4, 'BMX456', 1);
insert into contient_modele VALUES (5, 'VTT3000', 1);
insert into contient_modele VALUES (5, 'BMX123', 1);
insert into contient_modele VALUES (6, 'BMX456', 1);
insert into contient_modele VALUES (7, 'VTT3000', 5);
insert into contient_modele VALUES (7, 'CL98', 3);

# contient_piece
insert into contient_piece VALUES (1, 'C30', 1);
insert into contient_piece VALUES (2, 'DV133', 1);
insert into contient_piece VALUES (8, 'R03', 1);
insert into contient_piece VALUES (8, 'DV133', 2);
insert into contient_piece VALUES (9, 'F4', 1);
insert into contient_piece VALUES (10, 'F5', 10);
insert into contient_piece VALUES (10, 'S01', 7);












