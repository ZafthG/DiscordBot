-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Tempo de geração: 05-Out-2022 às 22:39
-- Versão do servidor: 5.7.36
-- versão do PHP: 7.4.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `botdb`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `foods`
--

DROP TABLE IF EXISTS `foods`;
CREATE TABLE IF NOT EXISTS `foods` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(200) NOT NULL,
  `nametext` varchar(100) DEFAULT NULL,
  `components` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=MyISAM AUTO_INCREMENT=32 DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `foods`
--

INSERT INTO `foods` (`id`, `name`, `nametext`, `components`) VALUES
(1, 'Pão de hot-dog com doce de banana', 'Pão de hot-dog com doce de banana', '0;5;7'),
(2, 'Pão hot-dog com doce de maçã', 'Pão de hot-dog com doce de maçã', '0;5;7'),
(3, 'Abacaxi', 'Abacaxi', '1;4;8'),
(4, 'Filé de frango à canadense', 'Filé de frango à canadense', '0;5;6;7;8'),
(5, 'Vegano: tomate recheado com farofinha de lentilha', 'Tomate recheado com farofinha de lentilha', '0;4'),
(6, 'Abóbora refogada', 'Abóbora refogada', '1;4'),
(7, 'Saladas: folhosae rabanete', 'Folhosas e rabanete', '2;4'),
(8, 'Gelatina de uva', 'Gelatina Nubank (uva)', '3;8'),
(9, 'Quibe de forma', 'Quibe de forma', '0;6;7;8;9;11'),
(10, 'Vegano: quibe de PTS', 'Quibe de PTS', '0;4;6;7;11'),
(11, 'Sopa raízes', 'Sopa de raízes', '1;4'),
(12, 'Saladas: folhosa e berinjela', 'Folhosas e berinjela', '2;4'),
(14, 'Saladas: folhosa e rabanete', 'Folhosas e Rabanete', '2;4'),
(15, 'Sopa de raízes', 'Sopa de raízes', '1;4'),
(18, 'Pão francês com queijo', 'Pão francês com queijo', '0;5;7'),
(19, 'Banana', 'Banana', '1;4'),
(20, 'Carne moída refogada', 'Carne moída refogada', '0;8;11'),
(21, 'Vegano: PTS refogada com brócolis', 'PTS refogada com brócolis', '0;4'),
(22, 'Polenta ao sugo', 'Polenta ao sugo', '1;4'),
(23, 'Saladas: folhosa e repolho roxo', 'Folhosas e repolho roxo', '2;4'),
(24, 'Maçã', 'Maçã', '1;4'),
(25, 'Peixe à dorê com limão', 'Peixe à dorê com limão', '0;5;7;8;9'),
(26, 'Vegano: escondidinho de ervilha seca', 'Escondidinho de ervilha seca', '0;4'),
(27, 'Sopa de legumes com carne', 'Sopa de legumes com carne', '1;7;8'),
(28, 'Saladas: folhosa e beterraba ralada', 'Folhosa e beterraba ralada', '2;4'),
(29, 'Saladas: folhosa e abobrinha cozida ao vinagrete', 'Folhosas e abobrinha cozida ao vinagrete', '2;4'),
(30, 'Vegano: Batata assada recheada com ervilha seca', 'Batata assada recheada com ervilha seca', '0;4;11'),
(31, 'Vegano: madalena de ervilha seca', NULL, NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `guilds`
--

DROP TABLE IF EXISTS `guilds`;
CREATE TABLE IF NOT EXISTS `guilds` (
  `id` bigint(20) NOT NULL,
  `tag` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `tag` (`tag`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `guilds`
--

INSERT INTO `guilds` (`id`, `tag`) VALUES
(1009142650156355585, 'TESTE');

-- --------------------------------------------------------

--
-- Estrutura da tabela `ru`
--

DROP TABLE IF EXISTS `ru`;
CREATE TABLE IF NOT EXISTS `ru` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `tag` varchar(16) NOT NULL,
  `name` varchar(60) NOT NULL,
  `url` varchar(400) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `tag` (`tag`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `ru`
--

INSERT INTO `ru` (`id`, `tag`, `name`, `url`) VALUES
(1, 'CENTRO', 'RU Central', 'https://pra.ufpr.br/ru/ru-central/'),
(2, 'POLITEC', 'RU Centro Politécnico', 'https://pra.ufpr.br/ru/ru-centro-politecnico/'),
(3, 'JARDIM', 'RU Jardim Botânico', 'https://pra.ufpr.br/ru/cardapio-ru-jardim-botanico/'),
(4, 'AGRARIAS', 'RU Agrárias', 'https://pra.ufpr.br/ru/cardapio-ru-agrarias/');

-- --------------------------------------------------------

--
-- Estrutura da tabela `text_channels`
--

DROP TABLE IF EXISTS `text_channels`;
CREATE TABLE IF NOT EXISTS `text_channels` (
  `id` bigint(20) NOT NULL,
  `guild_id` bigint(20) NOT NULL,
  `tag` varchar(10) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `tag` (`tag`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `text_channels`
--

INSERT INTO `text_channels` (`id`, `guild_id`, `tag`) VALUES
(1009142650709999688, 1009142650156355585, 'TEST_TCH');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
