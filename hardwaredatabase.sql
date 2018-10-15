-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 26, 2017 at 03:36 PM
-- Server version: 10.1.24-MariaDB
-- PHP Version: 7.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hardwaredatabase`
--

-- --------------------------------------------------------

--
-- Table structure for table `accounts`
--

CREATE TABLE `accounts` (
  `AccountId` int(11) NOT NULL,
  `Username` varchar(255) DEFAULT NULL,
  `Password` varchar(1000) DEFAULT NULL,
  `AccountType` varchar(10) DEFAULT NULL,
  `AccountStatus` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `accounts`
--

INSERT INTO `accounts` (`AccountId`, `Username`, `Password`, `AccountType`, `AccountStatus`) VALUES
(3, 'emplloyd', '12345', 'Employee', 'Active'),
(16, 'adlloyd', '12345', 'Admin', 'Active'),
(17, 'empone', '12345', 'Employee', 'Active'),
(18, 'lloydescoto2', '12345', 'Employee', 'Active'),
(21, 'newaccount', 'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA/GaKvf4xPU+hxUx4zNYplQAAAAACAAAAAAAQZgAAAAEAACAAAADFamelJ/PoEJ9R5S3mV5wV2tZdY2d4DXGV9y8S+udoVAAAAAAOgAAAAAIAACAAAAC5G+ynW/W1x6HbPuw45Sy+PPovoXWxp6IIXbbxQJVR2BAAAAAF/Uhd/LocbtPcMVZ8bxhmQAAAAEbv/eBfIwUGTgqx5WeT3fvjITybUpiHx9qR/l0rEJNQu1dy+K1NMlgIyxEVmbpor8UPrR295JoFQFluIvEBD+Y=', 'Admin', 'Active'),
(22, 'lloydescoto', 'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA/GaKvf4xPU+hxUx4zNYplQAAAAACAAAAAAAQZgAAAAEAACAAAAAgDAVLSC17U9LYtOW8cHcGWDSzcHORITiB2CDtRD4wAAAAAAAOgAAAAAIAACAAAABtZKR/H1xIh5HH3y5nmj83aDGJSzHG5YQaAG6wsZsADBAAAACQnYhUwEFcQt35fgdd3DulQAAAAMkx5PyV1FQhh6l/2e3Z8V2prD4GhoRwCcFyPb3nsWQObr56nXfrDB5n1fmmveXyceFxquMZPH2ZlRcBzCm9m+o=', 'Admin', 'Active'),
(23, 'newemp', 'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA/GaKvf4xPU+hxUx4zNYplQAAAAACAAAAAAAQZgAAAAEAACAAAAD15pkih9aVBifvsYIfPUPTfV2FkxoqVdpbQI011U+nhQAAAAAOgAAAAAIAACAAAADqlX9tQNiwHx1AcHtho2FZiN2U1Mc1CUVkzun65ILboBAAAABfQPYjgpPMfaBeUpTBD32EQAAAAGMsfIZHlWXqv4KxgWOf97WEZ6r0iixcn3GHl/ED6DtmRSFuM7NY8nT5zEpT+z3gVjFFQTO3av4F6zWNDI8HfkA=', 'Employee', 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `adminprofile`
--

CREATE TABLE `adminprofile` (
  `AdminId` int(11) NOT NULL,
  `AdminFName` varchar(255) DEFAULT NULL,
  `AdminLName` varchar(255) DEFAULT NULL,
  `AdminAddress` varchar(255) DEFAULT NULL,
  `AdminContactNumber` varchar(11) DEFAULT NULL,
  `AccountId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `adminprofile`
--

INSERT INTO `adminprofile` (`AdminId`, `AdminFName`, `AdminLName`, `AdminAddress`, `AdminContactNumber`, `AccountId`) VALUES
(3, 'Lloydy', 'Escoto', 'Mabalacat', '09268585092', 16),
(4, 'FirstNew', 'LastNew', 'Mabalacat', '09155230922', 21),
(5, 'Lloyd', 'Escoto', 'Mabalacat City', '09155230922', 22);

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

CREATE TABLE `customers` (
  `CustomerId` int(11) NOT NULL,
  `CustomerName` varchar(255) DEFAULT NULL,
  `CustomerEmail` varchar(255) DEFAULT NULL,
  `CustomerContactNumber` varchar(15) DEFAULT NULL,
  `CustomerAddress` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customers`
--

INSERT INTO `customers` (`CustomerId`, `CustomerName`, `CustomerEmail`, `CustomerContactNumber`, `CustomerAddress`) VALUES
(2, 'Lloyd', NULL, NULL, 'Mabalacat City'),
(3, 'Ivan', 'ivan@yahoo.com', '09155230922', 'Mabalacat City');

-- --------------------------------------------------------

--
-- Table structure for table `employeeprofile`
--

CREATE TABLE `employeeprofile` (
  `EmployeeId` int(11) NOT NULL,
  `EmployeeFName` varchar(255) DEFAULT NULL,
  `EmployeeLName` varchar(255) DEFAULT NULL,
  `EmployeeAddress` varchar(255) DEFAULT NULL,
  `EmployeeContactNumber` varchar(11) DEFAULT NULL,
  `AccountId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `employeeprofile`
--

INSERT INTO `employeeprofile` (`EmployeeId`, `EmployeeFName`, `EmployeeLName`, `EmployeeAddress`, `EmployeeContactNumber`, `AccountId`) VALUES
(1, 'Lloyd', 'Escoto', 'Mabalacat', '09268484026', 3),
(2, 'Empone', 'LastEmp', 'Mabalacat', '09092828281', 17),
(3, 'Lloyd', 'Escoto', 'Mabalacat City', '09155230922', 18),
(4, 'FirstEmp', 'LastEmp', 'Mabalacat City', '09155230922', 23);

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `ItemCode` int(11) NOT NULL,
  `ItemName` varchar(255) DEFAULT NULL,
  `ItemPrice` decimal(30,2) DEFAULT NULL,
  `ItemSellPrice` decimal(30,2) NOT NULL,
  `ItemUnit` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`ItemCode`, `ItemName`, `ItemPrice`, `ItemSellPrice`, `ItemUnit`) VALUES
(11, 'Nails', '10.00', '20.00', NULL),
(12, 'Screws', '20.00', '30.00', NULL),
(13, 'Paint', '30.00', '60.00', NULL),
(14, 'Paste', '30.00', '40.00', NULL),
(15, 'Hammer', '50.00', '100.00', NULL),
(16, 'Nail 1/4 Kg', '30.00', '50.00', NULL),
(17, 'Stone', '100.00', '200.00', 'pack'),
(18, 'Wall', '50.00', '100.00', 'box'),
(20, 'Ground', '20.00', '40.00', 'kl'),
(21, 'Sandbag', '30.00', '50.00', 'bag'),
(22, 'Sand', '20.00', '40.00', 'pack'),
(24, 'Rock', '20.00', '30.00', 'kl');

-- --------------------------------------------------------

--
-- Table structure for table `payables`
--

CREATE TABLE `payables` (
  `PayableId` int(11) NOT NULL,
  `PurchId` int(11) DEFAULT NULL,
  `Status` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `payables`
--

INSERT INTO `payables` (`PayableId`, `PurchId`, `Status`) VALUES
(1, 15, 'Unpaid'),
(2, 16, 'Unpaid'),
(3, 17, 'Unpaid'),
(4, 17, 'Unpaid'),
(5, 17, 'Unpaid'),
(6, 17, 'Unpaid'),
(7, 20, 'Unpaid'),
(8, 20, 'Unpaid'),
(9, 22, 'Unpaid'),
(10, 22, 'Unpaid'),
(11, 24, 'Unpaid'),
(12, 24, 'Unpaid'),
(13, 26, 'Unpaid');

-- --------------------------------------------------------

--
-- Table structure for table `purchases`
--

CREATE TABLE `purchases` (
  `PurchId` int(11) NOT NULL,
  `ItemCode` int(11) DEFAULT NULL,
  `ItemPrice` decimal(30,2) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `TransId` int(11) DEFAULT NULL,
  `SupplierId` int(11) DEFAULT NULL,
  `PaymentType` varchar(255) DEFAULT NULL,
  `ApplicableDiscount` int(11) DEFAULT NULL,
  `Status` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `purchases`
--

INSERT INTO `purchases` (`PurchId`, `ItemCode`, `ItemPrice`, `Quantity`, `TransId`, `SupplierId`, `PaymentType`, `ApplicableDiscount`, `Status`) VALUES
(5, 11, '10.00', 5, 25, 4, 'Cash', 0, 'Paid'),
(6, 12, '20.00', 10, 26, 5, 'Cash', 10, 'Paid'),
(7, 13, '30.00', 5, 26, 6, 'Cash', 0, 'Paid'),
(11, 13, '30.00', 5, 30, 10, 'Payable', 0, 'Unpaid'),
(12, 11, '10.00', 5, 31, 4, 'Payable', 0, 'Unpaid'),
(13, 13, '30.00', 5, 32, 4, 'Payable', 0, 'Unpaid'),
(14, 12, '20.00', 10, 33, 4, 'Payable', 0, 'Unpaid'),
(15, 14, '30.00', 10, 34, 4, 'Payable', 0, 'Unpaid'),
(16, 14, '30.00', 10, 35, 1, 'Payable', 10, 'Unpaid'),
(17, 11, '10.00', 5, 36, 4, 'Payable', 0, 'Unpaid'),
(18, 13, '30.00', 10, 36, 5, 'Payable', 0, 'Unpaid'),
(19, 15, '50.00', 10, 37, 10, 'Cash', 0, 'Paid'),
(20, 13, '30.00', 10, 37, 5, 'Payable', 0, 'Unpaid'),
(21, 15, '50.00', 10, 38, 4, 'Cash', 0, 'Paid'),
(22, 12, '20.00', 10, 38, 10, 'Payable', 0, 'Unpaid'),
(23, 13, '30.00', 10, 39, 4, 'Cash', 0, 'Paid'),
(24, 12, '20.00', 5, 39, 1, 'Payable', 0, 'Unpaid'),
(25, 13, '30.00', 10, 40, 5, 'Cash', 0, 'Paid'),
(26, 14, '30.00', 5, 40, 10, 'Payable', 10, 'Unpaid'),
(27, 11, '10.00', 5, 41, 4, 'Cash', 0, 'Paid'),
(28, 16, '30.00', 10, 60, 10, 'Cash', 0, 'Paid'),
(29, 15, '50.00', 3, 61, 10, 'Cash', 0, 'Paid'),
(30, 17, '100.00', 10, 62, 4, 'Cash', 0, 'Paid'),
(31, 13, '30.00', 10, 63, 5, 'Cash', 10, 'Paid'),
(32, 18, '50.00', 10, 63, 10, 'Cash', 0, 'Paid'),
(33, 11, '10.00', 10, 64, 5, 'Cash', 0, 'Paid'),
(34, 20, '20.00', 5, 67, 10, 'Cash', 0, 'Paid'),
(35, 20, '20.00', 1, 68, 10, 'Cash', 0, 'Paid'),
(36, 21, '30.00', 10, 69, 5, 'Cash', 10, 'Paid'),
(37, 22, '20.00', 10, 69, 5, 'Cash', 0, 'Paid'),
(38, 22, '20.00', 5, 71, 10, 'Cash', 0, 'Paid'),
(39, 24, '20.00', 10, 71, 10, 'Payable', 0, 'Unpaid'),
(40, 22, '20.00', 5, 72, 5, 'Cash', 0, 'Paid'),
(41, 24, '20.00', 10, 72, 10, 'Cash', 0, 'Paid');

-- --------------------------------------------------------

--
-- Table structure for table `purchasesreturns`
--

CREATE TABLE `purchasesreturns` (
  `PReturnId` int(11) NOT NULL,
  `ReturnQuantity` int(11) DEFAULT NULL,
  `ReturnDate` datetime DEFAULT NULL,
  `PurchId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `receivables`
--

CREATE TABLE `receivables` (
  `ReceivableId` int(11) NOT NULL,
  `SalesId` int(11) DEFAULT NULL,
  `Status` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `receivables`
--

INSERT INTO `receivables` (`ReceivableId`, `SalesId`, `Status`) VALUES
(1, 4, 'Unpaid'),
(2, 5, 'Unpaid'),
(3, 10, 'Unpaid'),
(4, 11, 'Unpaid'),
(5, 15, 'Unpaid'),
(6, 15, 'Unpaid'),
(7, 23, 'Unpaid');

-- --------------------------------------------------------

--
-- Table structure for table `sales`
--

CREATE TABLE `sales` (
  `SalesId` int(11) NOT NULL,
  `ItemCode` int(11) DEFAULT NULL,
  `ItemSellPrice` decimal(30,2) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `TransId` int(11) DEFAULT NULL,
  `CustomerId` int(11) DEFAULT NULL,
  `PaymentType` varchar(255) DEFAULT NULL,
  `ApplicableDiscount` int(11) DEFAULT NULL,
  `SalesInvoiceNumber` int(11) DEFAULT NULL,
  `EmployeeId` int(11) DEFAULT NULL,
  `Status` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sales`
--

INSERT INTO `sales` (`SalesId`, `ItemCode`, `ItemSellPrice`, `Quantity`, `TransId`, `CustomerId`, `PaymentType`, `ApplicableDiscount`, `SalesInvoiceNumber`, `EmployeeId`, `Status`) VALUES
(1, 15, '50.00', 5, 44, 2, 'Cash', 0, 1, 2, NULL),
(2, 15, '50.00', 5, 45, 2, 'Check', 5, 1, 2, NULL),
(3, 11, '10.00', 10, 46, 2, '', 10, 1, 1, NULL),
(4, 11, '10.00', 5, 49, 2, 'COD', 10, 2, 2, 'Unpaid'),
(5, 12, '20.00', 0, 49, 2, 'COD', 10, 3, 2, 'Unpaid'),
(6, 14, '30.00', 5, 50, 2, 'Cash', 0, 4, 2, 'Paid'),
(7, 13, '30.00', 0, 50, 2, 'Cash', 0, 5, 2, 'Paid'),
(8, 11, '10.00', 5, 51, 2, 'Cash', 0, 6, 2, 'Paid'),
(9, 12, '20.00', 0, 52, 2, 'Cash', 0, 7, 2, 'Paid'),
(10, 13, '30.00', 0, 53, 2, 'Receivable', 10, 8, 2, 'Unpaid'),
(11, 12, '20.00', 0, 53, 2, 'Receivable', 10, 9, 2, 'Unpaid'),
(12, 11, '10.00', 5, 54, 3, 'Cash', 0, 10, 2, 'Paid'),
(13, 12, '20.00', 0, 56, 3, 'Cash', 0, 11, 2, 'Returned'),
(14, 13, '30.00', 0, 56, 3, 'Cash', 0, 11, 2, 'Returned'),
(15, 12, '20.00', 0, 57, 2, 'Receivable', 10, 12, 2, 'Unpaid'),
(16, 13, '30.00', 0, 57, 2, 'Receivable', 10, 12, 2, 'Unpaid'),
(17, 12, '20.00', 0, 58, 3, 'Cash', 10, 13, 2, 'Paid'),
(18, 13, '30.00', 0, 58, 3, 'Cash', 10, 13, 2, 'Paid'),
(19, 12, '20.00', 0, 59, 3, 'Cash', 0, 14, 2, 'ReReturn'),
(20, 13, '30.00', 0, 59, 3, 'Cash', 0, 14, 2, 'ReReturn'),
(21, 11, '10.00', 5, 65, 3, 'Cash', 0, 15, 2, 'Paid'),
(22, 12, '20.00', 10, 65, 3, 'Cash', 0, 15, 2, 'Paid'),
(23, 13, '30.00', 20, 66, 3, 'Receivable', 0, 16, 2, 'Unpaid'),
(24, 20, '20.00', 1, 73, 3, 'Cash', 0, 17, 4, 'Paid'),
(25, 21, '30.00', 10, 73, 3, 'Cash', 0, 17, 4, 'Paid'),
(26, 22, '20.00', 2, 74, 3, 'Cash', 0, 18, 4, 'Paid'),
(27, 22, '20.00', 1, 75, 3, 'Cash', 0, 19, 4, 'Paid'),
(28, 22, '20.00', 2, 76, 3, 'Cash', 0, 20, 4, 'Paid'),
(29, 20, '20.00', 2, 77, 3, 'Cash', 0, 21, 4, 'Paid'),
(30, 21, '30.00', 1, 78, 3, 'Cash', 0, 22, 4, 'Paid'),
(31, 21, '30.00', 1, 79, 3, 'Cash', 0, 23, 4, 'Paid');

-- --------------------------------------------------------

--
-- Table structure for table `salesreturns`
--

CREATE TABLE `salesreturns` (
  `SReturnId` int(11) NOT NULL,
  `ReturnQuantity` int(11) DEFAULT NULL,
  `ReturnDate` datetime DEFAULT NULL,
  `SalesId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `stocks`
--

CREATE TABLE `stocks` (
  `StockId` int(11) NOT NULL,
  `Quantity` decimal(30,2) DEFAULT NULL,
  `UnitMeasurement` varchar(255) DEFAULT NULL,
  `Status` varchar(255) DEFAULT NULL,
  `ItemCode` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `stocks`
--

INSERT INTO `stocks` (`StockId`, `Quantity`, `UnitMeasurement`, `Status`, `ItemCode`) VALUES
(11, '10.00', '', 'Available', 11),
(12, '20.00', '', 'Available', 12),
(13, '40.00', '', 'Available', 13),
(14, '15.00', '', 'Available', 14),
(15, '3.00', '', 'Out-Of-Stock', 15),
(16, '10.00', '', 'Available', 16),
(17, '10.00', '', 'Available', 17),
(18, '10.00', '', 'Available', 18),
(20, '4.00', '1', 'Available', 20),
(21, '9.00', '1/2', 'Available', 20),
(22, '18.00', '1/4', 'Available', 20),
(23, '36.00', '1/8', 'Available', 20),
(24, '4.25', '1', 'Available', 21),
(25, '8.50', '1/2', 'Available', 21),
(26, '17.00', '1/4', 'Available', 21),
(27, '34.00', '1/8', 'Available', 21),
(28, '18.00', '1', 'Available', 22),
(29, '35.00', '1/2', 'Available', 22),
(30, '70.00', '1/4', 'Available', 22),
(31, '140.00', '1/8', 'Available', 22),
(36, '20.00', '1', 'Available', 24),
(37, '40.00', '1/2', 'Available', 24),
(38, '80.00', '1/4', 'Available', 24),
(39, '160.00', '1/8', 'Available', 24);

-- --------------------------------------------------------

--
-- Table structure for table `suppliers`
--

CREATE TABLE `suppliers` (
  `SupplierId` int(11) NOT NULL,
  `SupplierName` varchar(255) DEFAULT NULL,
  `SupplierEmail` varchar(255) DEFAULT NULL,
  `SupplierContactNumber` varchar(15) DEFAULT NULL,
  `SupplierAddress` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `suppliers`
--

INSERT INTO `suppliers` (`SupplierId`, `SupplierName`, `SupplierEmail`, `SupplierContactNumber`, `SupplierAddress`) VALUES
(1, 'Lloyd', NULL, NULL, 'Mabalacat City'),
(4, 'Ivan', 'lloydescoto24@yahoo.com', '09155230922', 'Mabalacat City'),
(5, 'Escoto', 'lloydescoto24@yahoo.com', '09155230922', 'Mabalacat City'),
(6, 'Loyd', '', '', ''),
(10, 'Master', 'master@yahoo.com', '09268585032', 'Mabalacat City');

-- --------------------------------------------------------

--
-- Table structure for table `transactions`
--

CREATE TABLE `transactions` (
  `TransId` int(11) NOT NULL,
  `TotalAmount` decimal(30,2) DEFAULT NULL,
  `TransType` varchar(255) DEFAULT NULL,
  `Date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `transactions`
--

INSERT INTO `transactions` (`TransId`, `TotalAmount`, `TransType`, `Date`) VALUES
(2, '200.00', 'Purchases', '2017-07-18 00:00:00'),
(3, '200.00', 'Purchases', '2017-07-18 00:00:00'),
(4, '200.00', 'Purchases', '2017-07-22 00:00:00'),
(5, '50.00', 'Sales', '2017-07-22 00:00:00'),
(6, '100.00', 'Sales', '2017-07-22 00:00:00'),
(7, '900.00', 'Purchases', '2017-07-22 00:00:00'),
(8, '150.00', 'Sales', '2017-07-22 00:00:00'),
(9, '150.00', 'Sales', '2017-07-22 00:00:00'),
(10, '200.00', 'Sales', '2017-08-08 00:00:00'),
(11, '200.00', 'Purchases', '2017-08-08 00:00:00'),
(12, '200.00', 'Purchases', '2017-08-19 00:00:00'),
(13, '800.00', 'Purchases', '2017-08-19 00:00:00'),
(14, '200.00', 'Sales', '2017-08-19 00:00:00'),
(15, '200.00', 'Sales', '2017-08-19 00:00:00'),
(16, '200.00', 'Purchases', '2017-08-19 00:00:00'),
(17, '200.00', 'Purchases', '2017-08-19 00:00:00'),
(20, '152.50', 'Purchases', '2017-08-26 00:00:00'),
(21, '152.50', 'Purchases', '2017-08-26 00:00:00'),
(22, '152.50', 'Purchases', '2017-08-26 00:00:00'),
(24, '230.00', 'Purchases', '2017-08-28 16:36:41'),
(25, '50.00', 'Purchases', '2017-08-28 16:47:41'),
(26, '330.00', 'Purchases', '2017-08-28 16:52:14'),
(27, '150.00', 'Purchases', '2017-08-28 19:13:15'),
(28, '100.00', 'Purchases', '2017-08-28 19:15:43'),
(29, '150.00', 'Purchases', '2017-08-28 19:16:19'),
(30, '150.00', 'Purchases', '2017-08-28 19:20:39'),
(31, '50.00', 'Purchases', '2017-08-28 19:26:45'),
(32, '250.00', 'Purchases', '2017-08-28 19:40:10'),
(33, '295.00', 'Purchases', '2017-08-28 19:46:15'),
(34, '500.00', 'Purchases', '2017-08-28 19:48:07'),
(35, '370.00', 'Purchases', '2017-08-28 19:51:23'),
(36, '350.00', 'Purchases', '2017-08-28 19:53:51'),
(37, '800.00', 'Purchases', '2017-08-28 19:56:07'),
(38, '700.00', 'Purchases', '2017-08-28 19:58:19'),
(39, '400.00', 'Purchases', '2017-08-28 20:03:31'),
(40, '435.00', 'Purchases', '2017-08-28 20:13:06'),
(41, '50.00', 'Purchases', '2017-08-28 20:31:38'),
(42, '500.00', 'Sales', '2017-08-28 20:48:40'),
(43, '500.00', 'Sales', '2017-08-28 21:38:51'),
(44, '500.00', 'Sales', '2017-08-28 22:03:55'),
(45, '500.00', 'Sales', '2017-08-28 22:10:34'),
(46, '180.00', 'Sales', '2017-08-28 22:20:30'),
(47, '450.00', 'Sales', '2017-08-28 23:53:50'),
(48, '315.00', 'Sales', '2017-08-28 23:55:15'),
(49, '225.00', 'Sales', '2017-08-28 23:56:49'),
(50, '500.00', 'Sales', '2017-08-28 23:57:38'),
(51, '100.00', 'Sales', '2017-08-29 00:00:30'),
(52, '300.00', 'Sales', '2017-08-29 00:03:34'),
(53, '405.00', 'Sales', '2017-08-29 00:04:10'),
(54, '100.00', 'Sales', '2017-08-29 00:05:26'),
(56, '200.00', 'Sales', '2017-08-29 01:14:24'),
(57, '405.00', 'Sales', '2017-08-29 01:15:22'),
(58, '810.00', 'Sales', '2017-08-29 08:53:09'),
(59, '450.00', 'Sales', '2017-09-12 02:46:30'),
(60, '300.00', 'Purchases', '2017-09-12 20:04:57'),
(61, '150.00', 'Purchases', '2017-09-16 23:02:42'),
(62, '1000.00', 'Purchases', '2017-09-19 23:26:55'),
(63, '770.00', 'Purchases', '2017-09-19 23:32:37'),
(64, '100.00', 'Purchases', '2017-09-19 23:33:48'),
(65, '400.00', 'Sales', '2017-09-20 00:10:03'),
(66, '1200.00', 'Sales', '2017-09-20 00:12:34'),
(67, '100.00', 'Purchases', '2017-09-23 23:37:26'),
(68, '20.00', 'Purchases', '2017-09-24 00:57:14'),
(69, '470.00', 'Purchases', '2017-09-26 00:46:52'),
(70, '150.00', 'Purchases', '2017-09-26 16:11:40'),
(71, '300.00', 'Purchases', '2017-09-26 16:18:57'),
(72, '300.00', 'Purchases', '2017-09-26 16:19:54'),
(73, '540.00', 'Sales', '2017-09-26 17:57:52'),
(74, '80.00', 'Sales', '2017-09-26 18:00:09'),
(75, '40.00', 'Sales', '2017-09-26 18:00:29'),
(76, '80.00', 'Sales', '2017-09-26 18:01:26'),
(77, '80.00', 'Sales', '2017-09-26 18:02:59'),
(78, '50.00', 'Sales', '2017-09-26 18:05:39'),
(79, '50.00', 'Sales', '2017-09-26 18:24:03');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`AccountId`);

--
-- Indexes for table `adminprofile`
--
ALTER TABLE `adminprofile`
  ADD PRIMARY KEY (`AdminId`),
  ADD KEY `AccountId` (`AccountId`);

--
-- Indexes for table `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`CustomerId`);

--
-- Indexes for table `employeeprofile`
--
ALTER TABLE `employeeprofile`
  ADD PRIMARY KEY (`EmployeeId`),
  ADD KEY `AccountId` (`AccountId`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`ItemCode`);

--
-- Indexes for table `payables`
--
ALTER TABLE `payables`
  ADD PRIMARY KEY (`PayableId`),
  ADD KEY `PurchId` (`PurchId`);

--
-- Indexes for table `purchases`
--
ALTER TABLE `purchases`
  ADD PRIMARY KEY (`PurchId`),
  ADD KEY `ItemCode` (`ItemCode`),
  ADD KEY `TransId` (`TransId`),
  ADD KEY `SupplierId` (`SupplierId`);

--
-- Indexes for table `purchasesreturns`
--
ALTER TABLE `purchasesreturns`
  ADD PRIMARY KEY (`PReturnId`),
  ADD KEY `PurchId` (`PurchId`);

--
-- Indexes for table `receivables`
--
ALTER TABLE `receivables`
  ADD PRIMARY KEY (`ReceivableId`),
  ADD KEY `SalesId` (`SalesId`);

--
-- Indexes for table `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`SalesId`),
  ADD KEY `ItemCode` (`ItemCode`),
  ADD KEY `TransId` (`TransId`),
  ADD KEY `CustomerId` (`CustomerId`),
  ADD KEY `EmployeeId` (`EmployeeId`);

--
-- Indexes for table `salesreturns`
--
ALTER TABLE `salesreturns`
  ADD PRIMARY KEY (`SReturnId`),
  ADD KEY `SalesId` (`SalesId`);

--
-- Indexes for table `stocks`
--
ALTER TABLE `stocks`
  ADD PRIMARY KEY (`StockId`),
  ADD KEY `ItemCode` (`ItemCode`);

--
-- Indexes for table `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`SupplierId`);

--
-- Indexes for table `transactions`
--
ALTER TABLE `transactions`
  ADD PRIMARY KEY (`TransId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `accounts`
--
ALTER TABLE `accounts`
  MODIFY `AccountId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;
--
-- AUTO_INCREMENT for table `adminprofile`
--
ALTER TABLE `adminprofile`
  MODIFY `AdminId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `customers`
--
ALTER TABLE `customers`
  MODIFY `CustomerId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `employeeprofile`
--
ALTER TABLE `employeeprofile`
  MODIFY `EmployeeId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `ItemCode` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
--
-- AUTO_INCREMENT for table `payables`
--
ALTER TABLE `payables`
  MODIFY `PayableId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT for table `purchases`
--
ALTER TABLE `purchases`
  MODIFY `PurchId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;
--
-- AUTO_INCREMENT for table `receivables`
--
ALTER TABLE `receivables`
  MODIFY `ReceivableId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT for table `sales`
--
ALTER TABLE `sales`
  MODIFY `SalesId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;
--
-- AUTO_INCREMENT for table `stocks`
--
ALTER TABLE `stocks`
  MODIFY `StockId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=40;
--
-- AUTO_INCREMENT for table `suppliers`
--
ALTER TABLE `suppliers`
  MODIFY `SupplierId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `transactions`
--
ALTER TABLE `transactions`
  MODIFY `TransId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=80;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `adminprofile`
--
ALTER TABLE `adminprofile`
  ADD CONSTRAINT `adminprofile_ibfk_1` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`AccountId`);

--
-- Constraints for table `employeeprofile`
--
ALTER TABLE `employeeprofile`
  ADD CONSTRAINT `employeeprofile_ibfk_1` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`AccountId`);

--
-- Constraints for table `payables`
--
ALTER TABLE `payables`
  ADD CONSTRAINT `payables_ibfk_1` FOREIGN KEY (`PurchId`) REFERENCES `purchases` (`PurchId`);

--
-- Constraints for table `purchases`
--
ALTER TABLE `purchases`
  ADD CONSTRAINT `purchases_ibfk_1` FOREIGN KEY (`ItemCode`) REFERENCES `items` (`ItemCode`),
  ADD CONSTRAINT `purchases_ibfk_2` FOREIGN KEY (`TransId`) REFERENCES `transactions` (`TransId`),
  ADD CONSTRAINT `purchases_ibfk_3` FOREIGN KEY (`SupplierId`) REFERENCES `suppliers` (`SupplierId`);

--
-- Constraints for table `receivables`
--
ALTER TABLE `receivables`
  ADD CONSTRAINT `receivables_ibfk_1` FOREIGN KEY (`SalesId`) REFERENCES `sales` (`SalesId`);

--
-- Constraints for table `sales`
--
ALTER TABLE `sales`
  ADD CONSTRAINT `sales_ibfk_1` FOREIGN KEY (`ItemCode`) REFERENCES `items` (`ItemCode`),
  ADD CONSTRAINT `sales_ibfk_2` FOREIGN KEY (`TransId`) REFERENCES `transactions` (`TransId`),
  ADD CONSTRAINT `sales_ibfk_3` FOREIGN KEY (`CustomerId`) REFERENCES `customers` (`CustomerId`),
  ADD CONSTRAINT `sales_ibfk_4` FOREIGN KEY (`EmployeeId`) REFERENCES `employeeprofile` (`EmployeeId`);

--
-- Constraints for table `stocks`
--
ALTER TABLE `stocks`
  ADD CONSTRAINT `stocks_ibfk_1` FOREIGN KEY (`ItemCode`) REFERENCES `items` (`ItemCode`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
