-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: sabpat702
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ai`
--

DROP TABLE IF EXISTS `ai`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ai` (
  `Id` int NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Description` text NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai`
--

LOCK TABLES `ai` WRITE;
/*!40000 ALTER TABLE `ai` DISABLE KEYS */;
/*!40000 ALTER TABLE `ai` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `armor`
--

DROP TABLE IF EXISTS `armor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `armor` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `DEF` int NOT NULL,
  `MDEF` int NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Type` int NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `armor`
--

LOCK TABLES `armor` WRITE;
/*!40000 ALTER TABLE `armor` DISABLE KEYS */;
/*!40000 ALTER TABLE `armor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `buff_and_debuff`
--

DROP TABLE IF EXISTS `buff_and_debuff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `buff_and_debuff` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `buff_and_debuff`
--

LOCK TABLES `buff_and_debuff` WRITE;
/*!40000 ALTER TABLE `buff_and_debuff` DISABLE KEYS */;
/*!40000 ALTER TABLE `buff_and_debuff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `Id` int NOT NULL,
  `Category` varchar(30) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Category_UNIQUE` (`Category`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comment` (
  `Id` int NOT NULL,
  `Content` text NOT NULL,
  `Date` date NOT NULL,
  `CommenterId` int NOT NULL,
  `PostId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_comment_post1_idx` (`PostId`),
  KEY `fk_comment_user1_idx` (`CommenterId`),
  CONSTRAINT `fk_comment_post1` FOREIGN KEY (`PostId`) REFERENCES `post` (`Id`),
  CONSTRAINT `fk_comment_user1` FOREIGN KEY (`CommenterId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `consumable`
--

DROP TABLE IF EXISTS `consumable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `consumable` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `SpecialEffect` varchar(200) NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consumable`
--

LOCK TABLES `consumable` WRITE;
/*!40000 ALTER TABLE `consumable` DISABLE KEYS */;
/*!40000 ALTER TABLE `consumable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dungeon`
--

DROP TABLE IF EXISTS `dungeon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dungeon` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Length` int NOT NULL,
  `Gold` int NOT NULL,
  `Experience` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dungeon`
--

LOCK TABLES `dungeon` WRITE;
/*!40000 ALTER TABLE `dungeon` DISABLE KEYS */;
/*!40000 ALTER TABLE `dungeon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `environment_hazard`
--

DROP TABLE IF EXISTS `environment_hazard`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `environment_hazard` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `ATK` int NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,2) NOT NULL,
  `SpecialEffect` varchar(200) NOT NULL,
  `Dungeon` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `environment_hazard`
--

LOCK TABLES `environment_hazard` WRITE;
/*!40000 ALTER TABLE `environment_hazard` DISABLE KEYS */;
/*!40000 ALTER TABLE `environment_hazard` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hero`
--

DROP TABLE IF EXISTS `hero`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hero` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `DEF` int NOT NULL,
  `MDEF` int NOT NULL,
  `HP` int NOT NULL,
  `SP` int NOT NULL,
  `MP` int NOT NULL,
  `EXP` int NOT NULL,
  `LVL` int NOT NULL,
  `Weapon` varchar(300) NOT NULL,
  `Armor` varchar(400) NOT NULL,
  `Skill` varchar(5000) NOT NULL,
  `Passive` varchar(3000) NOT NULL,
  `UserId` int DEFAULT NULL,
  `Race` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_hero_user1_idx` (`UserId`),
  CONSTRAINT `fk_hero_user1` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hero`
--

LOCK TABLES `hero` WRITE;
/*!40000 ALTER TABLE `hero` DISABLE KEYS */;
/*!40000 ALTER TABLE `hero` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `magic`
--

DROP TABLE IF EXISTS `magic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `magic` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `ATK` int NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,2) NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Range` varchar(5) NOT NULL,
  `MPCost` int NOT NULL,
  `CD` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magic`
--

LOCK TABLES `magic` WRITE;
/*!40000 ALTER TABLE `magic` DISABLE KEYS */;
/*!40000 ALTER TABLE `magic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `monster`
--

DROP TABLE IF EXISTS `monster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `monster` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `HP` int NOT NULL,
  `DEF` int NOT NULL,
  `MDEF` int NOT NULL,
  `ATK` int NOT NULL,
  `SP` int NOT NULL,
  `MP` int NOT NULL,
  `Passive` varchar(1000) NOT NULL,
  `Skill` varchar(1000) NOT NULL,
  `Magic` varchar(1000) NOT NULL,
  `Race` varchar(100) NOT NULL,
  `Ai` varchar(40) NOT NULL,
  `Dungeon` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `monster`
--

LOCK TABLES `monster` WRITE;
/*!40000 ALTER TABLE `monster` DISABLE KEYS */;
/*!40000 ALTER TABLE `monster` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `passive`
--

DROP TABLE IF EXISTS `passive`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `passive` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `passive`
--

LOCK TABLES `passive` WRITE;
/*!40000 ALTER TABLE `passive` DISABLE KEYS */;
/*!40000 ALTER TABLE `passive` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `post`
--

DROP TABLE IF EXISTS `post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `post` (
  `Id` int NOT NULL,
  `Title` varchar(100) NOT NULL,
  `Content` text NOT NULL,
  `Date` date NOT NULL,
  `CategoryId` int NOT NULL,
  `PosterId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_post_category_idx` (`CategoryId`),
  KEY `fk_post_user1_idx` (`PosterId`),
  CONSTRAINT `fk_post_category` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`Id`),
  CONSTRAINT `fk_post_user1` FOREIGN KEY (`PosterId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `post`
--

LOCK TABLES `post` WRITE;
/*!40000 ALTER TABLE `post` DISABLE KEYS */;
/*!40000 ALTER TABLE `post` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `race`
--

DROP TABLE IF EXISTS `race`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `race` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Fatal` varchar(100) NOT NULL,
  `Weak` varchar(100) NOT NULL,
  `Resist` varchar(100) NOT NULL,
  `Endure` varchar(100) NOT NULL,
  `Null` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `race`
--

LOCK TABLES `race` WRITE;
/*!40000 ALTER TABLE `race` DISABLE KEYS */;
/*!40000 ALTER TABLE `race` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skill`
--

DROP TABLE IF EXISTS `skill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `skill` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,2) NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Range` varchar(5) NOT NULL,
  `SPCost` int NOT NULL,
  `CD` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skill`
--

LOCK TABLES `skill` WRITE;
/*!40000 ALTER TABLE `skill` DISABLE KEYS */;
/*!40000 ALTER TABLE `skill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `special_effect`
--

DROP TABLE IF EXISTS `special_effect`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `special_effect` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `special_effect`
--

LOCK TABLES `special_effect` WRITE;
/*!40000 ALTER TABLE `special_effect` DISABLE KEYS */;
/*!40000 ALTER TABLE `special_effect` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `Id` int NOT NULL,
  `UserName` varchar(30) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `weapon`
--

DROP TABLE IF EXISTS `weapon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `weapon` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `ATK` int NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,2) NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Range` int NOT NULL,
  `SkillCompatibility` varchar(5) NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `weapon`
--

LOCK TABLES `weapon` WRITE;
/*!40000 ALTER TABLE `weapon` DISABLE KEYS */;
/*!40000 ALTER TABLE `weapon` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-23 12:37:58
