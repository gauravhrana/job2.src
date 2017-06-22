'use strict';

var app = angular.module('rootAppShell', [
          'ngResource'
        , 'ngSanitize'
        , 'ngRoute'
		, 'ngCookies'
        , 'ngTouch'
        , 'ui.select2'
        , 'ui.bootstrap'
        , 'btford.markdown'
        , 'ngNumeraljs'
        , 'restangular'
		, 'kendo.directives'
        , 'ui.ace'
		, 'ui.grid', 'cgBusy', 'ui.grid.cellNav', 'ui.grid.edit', 'ui.grid.resizeColumns', 'ui.grid.exporter', 'ui.grid.pinning', 'ui.grid.selection', 'ui.grid.moveColumns', 'ui.grid.pagination'
		, 'ngTable'
        , 'adf'
        , 'adf.widget.clock'
        , 'LocalStorageModule'
])
    .config(function ($httpProvider, $routeProvider, $parseProvider, $logProvider, RestangularProvider, dashboardProvider, localStorageServiceProvider) {


        RestangularProvider.setBaseUrl('../../apiV2/rest/');
        RestangularProvider.setDefaultHeaders({ 'Content-Type': 'application/json' });

        $logProvider.debugEnabled(true);

        localStorageServiceProvider.setPrefix('adf.clock');
        dashboardProvider.structure('4-8', {
            rows: [{
                columns: [{
                    styleClass: 'col-md-4',
                    widgets: []
                }, {
                    styleClass: 'col-md-8',
                    widgets: []
                }]
            }]
        })

        //$parseProvider.unwrapPromises(true);

        //Route mapping
        $routeProvider

            // core
            .when('/main', { templateUrl: '../../app/BM/CapitalMarkets/views/main.html', controller: 'mainCMCtrl', title: 'main', caseInsensitiveMatch: true })
            .when('/:moduleName/main', { templateUrl: '../../app/BM/CapitalMarkets/views/main.html', controller: 'mainCMCtrl', caseInsensitiveMatch: true })

            .when('/login', { templateUrl: '../../app/core/views/login.html', controller: 'loginCtrl' })
            .when('/logout', { templateUrl: '../../app/core/views/logout.html', controller: 'logoutCtrl' })

            .when('/testDatePicker', { templateUrl: '../../app/BM/CapitalMarkets/views/testDatePicker.html', controller: 'testDatePickerCtrl' })

            .when('/:moduleName/test-restangular/search', { templateUrl: '../../app/examples/views/testRestangular.html', controller: 'testRestangularCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/test-restangular/save/:detailId', { templateUrl: '../../app/examples/views/testRestangularSave.html', controller: 'testRestangularSaveCtrl', caseInsensitiveMatch: true })

            //FundXPortfolio
            .when('/:moduleName/FundXPortfolio', { templateUrl: '../../app/BM/CapitalMarkets/views/FundXPortfolio.html', controller: 'fundXPortfolioCtrl', caseInsensitiveMatch: true })

            // Fund X Legal Entity
            .when('/:moduleName/FundXLegalEntity', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/CrossReference/FundXLegalEntity.html', controller: 'fundXLegalEntityCtrl', caseInsensitiveMatch: true })

            // Portfolio X Custodian Account
            .when('/:moduleName/PortfolioXCustodianAccount', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/CrossReference/PortfolioXCustodianAccount.html', controller: 'portfolioXCustodianAccountCtrl', caseInsensitiveMatch: true })

            //PriceScheduleXPriceList
            .when('/:moduleName/PriceScheduleXPriceList', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/CrossReference/PriceScheduleXPriceList.html', controller: 'priceScheduleXPriceListCtrl', caseInsensitiveMatch: true })

            //CustodianAccount
            .when('/:moduleName/CustodianAccount/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCustodianAccount.html', controller: 'listTableCustodianAccountCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CustodianAccount/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCustodianAccount.html', controller: 'detailCustodianAccountCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CustodianAccount/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCustodianAccount.html', controller: 'saveCustodianAccountCtrl', caseInsensitiveMatch: true })
            
            //TransactionType
            .when('/:moduleName/TransactionType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTransactionType.html', controller: 'listTableTransactionTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTransactionType.html', controller: 'detailTransactionTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTransactionType.html', controller: 'saveTransactionTypeCtrl', caseInsensitiveMatch: true })

            //Position
            .when('/:moduleName/Position/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePosition.html', controller: 'listTablePositionCtrl', caseInsensitiveMatch: true, title: 'Position' })
            .when('/:moduleName/Position/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPosition.html', controller: 'detailPositionCtrl', caseInsensitiveMatch: true, title: 'Position' })
            .when('/:moduleName/Position/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePosition.html', controller: 'savePositionCtrl', caseInsensitiveMatch: true, title: 'Position' })

            //Custodian
            .when('/:moduleName/Custodian/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCustodian.html', controller: 'listTableCustodianCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Custodian/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCustodian.html', controller: 'detailCustodianCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Custodian/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCustodian.html', controller: 'saveCustodianCtrl', caseInsensitiveMatch: true })

            //BusinessCalender
            .when('/:moduleName/BusinessCalender/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableBusinessCalender.html', controller: 'listTableBusinessCalenderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BusinessCalender/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailBusinessCalender.html', controller: 'detailBusinessCalenderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BusinessCalender/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveBusinessCalender.html', controller: 'saveBusinessCalenderCtrl', caseInsensitiveMatch: true })

             //AccountingCalender
            .when('/:moduleName/AccountingCalender/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAccountingCalender.html', controller: 'listTableAccountingCalenderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountingCalender/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAccountingCalender.html', controller: 'detailAccountingCalenderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountingCalender/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAccountingCalender.html', controller: 'saveAccountingCalenderCtrl', caseInsensitiveMatch: true })
            
            //FiscalCalender
            .when('/:moduleName/FiscalCalender/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableFiscalCalender.html', controller: 'listTableFiscalCalenderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FiscalCalender/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailFiscalCalender.html', controller: 'detailFiscalCalenderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FiscalCalender/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveFiscalCalender.html', controller: 'saveFiscalCalenderCtrl', caseInsensitiveMatch: true })

            //Broker
            .when('/:moduleName/broker/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableBroker.html', controller: 'listTableBrokerCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/broker/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailBroker.html', controller: 'detailBrokerCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/broker/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveBroker.html', controller: 'saveBrokerCtrl', caseInsensitiveMatch: true })

            //InvestmentType
            .when('/:moduleName/investmentType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableInvestmentType.html', controller: 'listTableInvestmentTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/investmentType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailInvestmentType.html', controller: 'detailInvestmentTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/investmentType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveInvestmentType.html', controller: 'saveInvestmentTypeCtrl', caseInsensitiveMatch: true })

             //AssetType
            .when('/:moduleName/assetType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAssetType.html', controller: 'listTableAssetTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/assetType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAssetType.html', controller: 'detailAssetTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/assetType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAssetType.html', controller: 'saveAssetTypeCtrl', caseInsensitiveMatch: true })

            ////Strategy
            .when('/:moduleName/strategy/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableStrategy.html', controller: 'listTableStrategyCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/strategy/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailStrategy.html', controller: 'detailStrategyCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/strategy/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveStrategy.html', controller: 'saveStrategyCtrl', caseInsensitiveMatch: true })

            //SecurityClass
            .when('/:moduleName/SecurityClass/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurityClass.html', controller: 'listTableSecurityClassCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityClass/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurityClass.html', controller: 'detailSecurityClassCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityClass/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurityClass.html', controller: 'saveSecurityClassCtrl', caseInsensitiveMatch: true })

            //Portfolio
            .when('/:moduleName/portfolio/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePortfolio.html', controller: 'listTablePortfolioCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/portfolio/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPortfolio.html', controller: 'detailPortfolioCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/portfolio/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePortfolio.html', controller: 'savePortfolioCtrl', caseInsensitiveMatch: true })

            //Fund
            .when('/:moduleName/fund/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableFund.html', controller: 'listTableFundCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/fund/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailFund.html', controller: 'detailFundCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/fund/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveFund.html', controller: 'saveFundCtrl', caseInsensitiveMatch: true })

             //LegalEntity
            .when('/:moduleName/legalEntity/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableLegalEntity.html', controller: 'listTableLegalEntityCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/legalEntity/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailLegalEntity.html', controller: 'detailLegalEntityCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/legalEntity/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveLegalEntity.html', controller: 'saveLegalEntityCtrl', caseInsensitiveMatch: true })

            //ManagementFirm
            .when('/:moduleName/ManagementFirm/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableManagementFirm.html', controller: 'listTableManagementFirmCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ManagementFirm/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailManagementFirm.html', controller: 'detailManagementFirmCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ManagementFirm/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveManagementFirm.html', controller: 'saveManagementFirmCtrl', caseInsensitiveMatch: true })

            //Equity
            .when('/:moduleName/Equity/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableEquity.html', controller: 'listTableEquityCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Equity/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailEquity.html', controller: 'detailEquityCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Equity/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveEquity.html', controller: 'saveEquityCtrl', caseInsensitiveMatch: true })

            //Bond
            .when('/:moduleName/Bond/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableBond.html', controller: 'listTableBondCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Bond/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailBond.html', controller: 'detailBondCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Bond/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveBond.html', controller: 'saveBondCtrl', caseInsensitiveMatch: true })

            //AssetBacked
            .when('/:moduleName/AssetBacked/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAssetBacked.html', controller: 'listTableAssetBackedCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AssetBacked/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAssetBacked.html', controller: 'detailAssetBackedCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AssetBacked/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAssetBacked.html', controller: 'saveAssetBackedCtrl', caseInsensitiveMatch: true })

             //Future
            .when('/:moduleName/Future/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableFuture.html', controller: 'listTableFutureCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Future/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailFuturehtml', controller: 'detailFutureCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Future/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveFuture.html', controller: 'saveFutureCtrl', caseInsensitiveMatch: true })

            //ForwardFXContract
            .when('/:moduleName/ForwardFXContract/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableForwardFXContract.html', controller: 'listTableForwardFXContractCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ForwardFXContract/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailForwardFXContracthtml', controller: 'detailForwardFXContractCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ForwardFXContract/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveForwardFXContract.html', controller: 'saveForwardFXContractCtrl', caseInsensitiveMatch: true })

            //MediumOfExchange
            .when('/:moduleName/MediumOfExchange/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableMediumOfExchange.html', controller: 'listTableMediumOfExchangeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MediumOfExchange/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailMediumOfExchangehtml', controller: 'detailMediumOfExchangeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MediumOfExchange/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveMediumOfExchange.html', controller: 'saveMediumOfExchangeCtrl', caseInsensitiveMatch: true })

            //ForwardCash
            .when('/:moduleName/ForwardCash/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableForwardCash.html', controller: 'listTableForwardCashCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ForwardCash/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailForwardCash.html', controller: 'detailForwardCashCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ForwardCash/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveForwardCash.html', controller: 'saveForwardCashCtrl', caseInsensitiveMatch: true })

            //Cash
            .when('/:moduleName/Cash/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCash.html', controller: 'listTableCashCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Cash/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCashhtml', controller: 'detailCashCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Cash/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCash.html', controller: 'saveCashCtrl', caseInsensitiveMatch: true })

            //STIF
            .when('/:moduleName/STIF/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSTIF.html', controller: 'listTableSTIFCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/STIF/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSTIFhtml', controller: 'detailSTIFCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/STIF/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSTIF.html', controller: 'saveSTIFCtrl', caseInsensitiveMatch: true })

            //PerformanceKey
            .when('/:moduleName/PerformanceKey/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePerformanceKey.html', controller: 'listTablePerformanceKeyCtrl', caseInsensitiveMatch: true, title: 'PerformanceKey' })
            .when('/:moduleName/PerformanceKey/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPerformanceKey.html', controller: 'detailPerformanceKeyCtrl', caseInsensitiveMatch: true, title: 'PerformanceKey' })
            .when('/:moduleName/PerformanceKey/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePerformanceKey.html', controller: 'savePerformanceKeyCtrl', caseInsensitiveMatch: true, title: 'PerformanceKey' })

            //PerformanceParameters
            .when('/:moduleName/PerformanceParameters/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePerformanceParameters.html', controller: 'listTablePerformanceParametersCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PerformanceParameters/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPerformanceParameters.html', controller: 'detailPerformanceParametersCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PerformanceParameters/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePerformanceParameters.html', controller: 'savePerformanceParametersCtrl', caseInsensitiveMatch: true })

            //TWRSet
            .when('/:moduleName/TWRSet/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTWRSet.html', controller: 'listTableTWRSetCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TWRSet/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTWRSet.html', controller: 'detailTWRSetCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TWRSet/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTWRSet.html', controller: 'saveTWRSetCtrl', caseInsensitiveMatch: true })

            //TWRBatchProcessing
            .when('/:moduleName/TWRBatchProcessing/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTWRBatchProcessing.html', controller: 'listTableTWRBatchProcessingCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TWRBatchProcessing/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTWRBatchProcessing.html', controller: 'detailTWRBatchProcessingCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TWRBatchProcessing/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTWRBatchProcessing.html', controller: 'saveTWRBatchProcessingCtrl', caseInsensitiveMatch: true })

             //SwapInvestment
            .when('/:moduleName/SwapInvestment/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSwapInvestment.html', controller: 'listTableSwapInvestmentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SwapInvestment/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSwapInvestment.html', controller: 'detailSwapInvestmentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SwapInvestment/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSwapInvestment.html', controller: 'saveSwapInvestmentCtrl', caseInsensitiveMatch: true })

             //Financing
            .when('/:moduleName/Financing/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableFinancing.html', controller: 'listTableFinancingCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Financing/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailFinancing.html', controller: 'detailFinancingCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Financing/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveFinancing.html', controller: 'saveFinancingCtrl', caseInsensitiveMatch: true })

            //CreditDefaultSwap
            .when('/:moduleName/CreditDefaultSwap/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCreditDefaultSwap.html', controller: 'listTableCreditDefaultSwapCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditDefaultSwap/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCreditDefaultSwap.html', controller: 'detailCreditDefaultSwapCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditDefaultSwap/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCreditDefaultSwap.html', controller: 'saveCreditDefaultSwapCtrl', caseInsensitiveMatch: true })

            //CreditDefaultSwapIndex
            .when('/:moduleName/CreditDefaultSwapIndex/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCreditDefaultSwapIndex.html', controller: 'listTableCreditDefaultSwapIndexCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditDefaultSwapIndex/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCreditDefaultSwapIndex.html', controller: 'detailCreditDefaultSwapIndexCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditDefaultSwapIndex/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCreditDefaultSwapIndex.html', controller: 'saveCreditDefaultSwapIndexCtrl', caseInsensitiveMatch: true })

            //InvestmentPrices
            .when('/:moduleName/InvestmentPrices/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableInvestmentPrices.html', controller: 'listTableInvestmentPricesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/InvestmentPrices/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailInvestmentPrices.html', controller: 'detailInvestmentPricesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/InvestmentPrices/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveInvestmentPrices.html', controller: 'saveInvestmentPricesCtrl', caseInsensitiveMatch: true })

            //FundPrices
            .when('/:moduleName/FundPrices/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableFundPrices.html', controller: 'listTableFundPricesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FundPrices/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailFundPrices.html', controller: 'detailFundPricesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FundPrices/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveFundPrices.html', controller: 'saveFundPricesCtrl', caseInsensitiveMatch: true })

             //Annotations
            .when('/:moduleName/Annotations/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAnnotations.html', controller: 'listTableAnnotationsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Annotations/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAnnotations.html', controller: 'detailAnnotationsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Annotations/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAnnotations.html', controller: 'saveAnnotationsCtrl', caseInsensitiveMatch: true })

             //PriceType
            .when('/:moduleName/PriceType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePriceType.html', controller: 'listTablePriceTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPriceType.html', controller: 'detailPriceTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePriceType.html', controller: 'savePriceTypeCtrl', caseInsensitiveMatch: true })

             //PriceMarket
            .when('/:moduleName/PriceMarket/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePriceMarket.html', controller: 'listTablePriceMarketCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceMarket/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPriceMarket.html', controller: 'detailPriceMarketCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceMarket/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePriceMarket.html', controller: 'savePriceMarketCtrl', caseInsensitiveMatch: true })

             //PriceProvider
            .when('/:moduleName/PriceProvider/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePriceProvider.html', controller: 'listTablePriceProviderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceProvider/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPriceProvider.html', controller: 'detailPriceProviderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceProvider/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePriceProvider.html', controller: 'savePriceProviderCtrl', caseInsensitiveMatch: true })

            //PriceSource
            .when('/:moduleName/PriceSource/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePriceSource.html', controller: 'listTablePriceSourceCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceSource/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPriceSource.html', controller: 'detailPriceSourceCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceSource/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePriceSource.html', controller: 'savePriceSourceCtrl', caseInsensitiveMatch: true })

            //PriceList
            .when('/:moduleName/PriceList/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePriceList.html', controller: 'listTablePriceListCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceList/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPriceList.html', controller: 'detailPriceListCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceList/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePriceList.html', controller: 'savePriceListCtrl', caseInsensitiveMatch: true })

            //PriceSchedule
            .when('/:moduleName/PriceSchedule/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePriceSchedule.html', controller: 'listTablePriceScheduleCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceSchedule/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPriceSchedule.html', controller: 'detailPriceScheduleCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PriceSchedule/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePriceSchedule.html', controller: 'savePriceScheduleCtrl', caseInsensitiveMatch: true })

            //InvestingFeeder
            .when('/:moduleName/InvestingFeeder/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableInvestingFeeder.html', controller: 'listTableInvestingFeederCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/InvestingFeeder/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailInvestingFeeder.html', controller: 'detailInvestingFeederCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/InvestingFeeder/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveInvestingFeeder.html', controller: 'saveInvestingFeederCtrl', caseInsensitiveMatch: true })

             //Class
            .when('/:moduleName/Class/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableClass.html', controller: 'listTableClassCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Class/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailClass.html', controller: 'detailClassCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Class/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveClass.html', controller: 'saveClassCtrl', caseInsensitiveMatch: true })

             //SubClass
            .when('/:moduleName/SubClass/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSubClass.html', controller: 'listTableSubClassCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SubClass/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSubClass.html', controller: 'detailSubClassCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SubClass/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSubClass.html', controller: 'saveSubClassCtrl', caseInsensitiveMatch: true })

             //Series
            .when('/:moduleName/Series/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSeries.html', controller: 'listTableSeriesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Series/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSeries.html', controller: 'detailSeriesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Series/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSeries.html', controller: 'saveSeriesCtrl', caseInsensitiveMatch: true })

             //AllocationGroup
            .when('/:moduleName/AllocationGroup/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAllocationGroup.html', controller: 'listTableAllocationGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AllocationGroup/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAllocationGroup.html', controller: 'detailAllocationGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AllocationGroup/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAllocationGroup.html', controller: 'saveAllocationGroupCtrl', caseInsensitiveMatch: true })

             //AccountingParameters
            .when('/:moduleName/AccountingParameters/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAccountingParameters.html', controller: 'listTableAccountingParametersCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountingParameters/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAccountingParameters.html', controller: 'detailAccountingParametersCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountingParameters/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAccountingParameters.html', controller: 'saveAccountingParametersCtrl', caseInsensitiveMatch: true })

            //AccountingView
            .when('/:moduleName/AccountingView/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAccountingView.html', controller: 'listTableAccountingViewCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountingView/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAccountingView.html', controller: 'detailAccountingViewCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountingView/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAccountingView.html', controller: 'saveAccountingViewCtrl', caseInsensitiveMatch: true })

            //AccountSpecificType
            .when('/:moduleName/AccountSpecificType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAccountSpecificType.html', controller: 'listTableAccountSpecificTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountSpecificType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAccountSpecificType.html', controller: 'detailAccountSpecificTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountSpecificType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAccountSpecificType.html', controller: 'saveAccountSpecificTypeCtrl', caseInsensitiveMatch: true })

            //AccountSubType
            .when('/:moduleName/AccountSubType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAccountSubType.html', controller: 'listTableAccountSubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountSubType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAccountSubType.html', controller: 'detailAccountSubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountSubType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAccountSubType.html', controller: 'saveAccountSubTypeCtrl', caseInsensitiveMatch: true })

            //Amortization
            .when('/:moduleName/Amortization/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAmortization.html', controller: 'listTableAmortizationCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Amortization/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAmortization.html', controller: 'detailAmortizationCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Amortization/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAmortization.html', controller: 'saveAmortizationCtrl', caseInsensitiveMatch: true })

            //AutomatedFreezepoint
            .when('/:moduleName/AutomatedFreezepoint/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAutomatedFreezepoint.html', controller: 'listTableAutomatedFreezepointCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AutomatedFreezepoint/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAutomatedFreezepoint.html', controller: 'detailAutomatedFreezepointCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AutomatedFreezepoint/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAutomatedFreezepoint.html', controller: 'saveAutomatedFreezepointCtrl', caseInsensitiveMatch: true })

            //ChartOfAccounts
            .when('/:moduleName/ChartOfAccounts/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableChartOfAccounts.html', controller: 'listTableChartOfAccountsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ChartOfAccounts/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailChartOfAccounts.html', controller: 'detailChartOfAccountsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ChartOfAccounts/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveChartOfAccounts.html', controller: 'saveChartOfAccountsCtrl', caseInsensitiveMatch: true })

            //FreezePoints
            .when('/:moduleName/FreezePoints/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableFreezePoints.html', controller: 'listTableFreezePointsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FreezePoints/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailFreezePoints.html', controller: 'detailFreezePointsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FreezePoints/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveFreezePoints.html', controller: 'saveFreezePointsCtrl', caseInsensitiveMatch: true })

            //InventoryState
            .when('/:moduleName/InventoryState/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableInventoryState.html', controller: 'listTableInventoryStateCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/InventoryState/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailInventoryState.html', controller: 'detailInventoryStateCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/InventoryState/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveInventoryState.html', controller: 'saveInventoryStateCtrl', caseInsensitiveMatch: true })

            //LockdownPools
            .when('/:moduleName/LockdownPools/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableLockdownPools.html', controller: 'listTableLockdownPoolsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/LockdownPools/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailLockdownPools.html', controller: 'detailLockdownPoolsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/LockdownPools/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveLockdownPools.html', controller: 'saveLockdownPoolsCtrl', caseInsensitiveMatch: true })

            //TaxAccountType
            .when('/:moduleName/TaxAccountType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTaxAccountType.html', controller: 'listTableTaxAccountTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TaxAccountType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTaxAccountType.html', controller: 'detailTaxAccountTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TaxAccountType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTaxAccountType.html', controller: 'saveTaxAccountTypeCtrl', caseInsensitiveMatch: true })

            //TaxStatus
            .when('/:moduleName/TaxStatus/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTaxStatus.html', controller: 'listTableTaxStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TaxStatus/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTaxStatus.html', controller: 'detailTaxStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TaxStatus/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTaxStatus.html', controller: 'saveTaxStatusCtrl', caseInsensitiveMatch: true })

            //WithholdingTaxType
            .when('/:moduleName/WithholdingTaxType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableWithholdingTaxType.html', controller: 'listTableWithholdingTaxTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/WithholdingTaxType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailWithholdingTaxType.html', controller: 'detailWithholdingTaxTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/WithholdingTaxType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveWithholdingTaxType.html', controller: 'saveWithholdingTaxTypeCtrl', caseInsensitiveMatch: true })

            //PortfolioGroupRules
            .when('/:moduleName/PortfolioGroupRules/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePortfolioGroupRules.html', controller: 'listTablePortfolioGroupRulesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PortfolioGroupRules/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPortfolioGroupRules.html', controller: 'detailPortfolioGroupRulesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PortfolioGroupRules/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePortfolioGroupRules.html', controller: 'savePortfolioGroupRulesCtrl', caseInsensitiveMatch: true })

            //Trader
            .when('/:moduleName/Trader/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTrader.html', controller: 'listTableTraderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Trader/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTrader.html', controller: 'detailTraderCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Trader/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTrader.html', controller: 'saveTraderCtrl', caseInsensitiveMatch: true })

            //AccountAdministrator
            .when('/:moduleName/AccountAdministrator/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAccountAdministrator.html', controller: 'listTableAccountAdministratorCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountAdministrator/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAccountAdministrator.html', controller: 'detailAccountAdministratorCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountAdministrator/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAccountAdministrator.html', controller: 'saveAccountAdministratorCtrl', caseInsensitiveMatch: true })

            //AccountExcecutive
            .when('/:moduleName/AccountExcecutive/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAccountExcecutive.html', controller: 'listTableAccountExcecutiveCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountExcecutive/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAccountExcecutive.html', controller: 'detailAccountExcecutiveCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccountExcecutive/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAccountExcecutive.html', controller: 'saveAccountExcecutiveCtrl', caseInsensitiveMatch: true })

            //DeliveryAgent
            .when('/:moduleName/DeliveryAgent/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableDeliveryAgent.html', controller: 'listTableDeliveryAgentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/DeliveryAgent/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailDeliveryAgent.html', controller: 'detailDeliveryAgentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/DeliveryAgent/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveDeliveryAgent.html', controller: 'saveDeliveryAgentCtrl', caseInsensitiveMatch: true })

            //Issuer
            .when('/:moduleName/Issuer/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableIssuer.html', controller: 'listTableIssuerCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Issuer/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailIssuer.html', controller: 'detailIssuerCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Issuer/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveIssuer.html', controller: 'saveIssuerCtrl', caseInsensitiveMatch: true })

            //Exchange
            .when('/:moduleName/Exchange/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableExchange.html', controller: 'listTableExchangeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Exchange/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailExchange.html', controller: 'detailExchangeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Exchange/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveExchange.html', controller: 'saveExchangeCtrl', caseInsensitiveMatch: true })

            //AgentBank
            .when('/:moduleName/AgentBank/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAgentBank.html', controller: 'listTableAgentBankCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AgentBank/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAgentBank.html', controller: 'detailAgentBankCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AgentBank/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAgentBank.html', controller: 'saveAgentBankCtrl', caseInsensitiveMatch: true })

            //Analyst
            .when('/:moduleName/Analyst/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAnalyst.html', controller: 'listTableAnalystCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Analyst/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAnalyst.html', controller: 'detailAnalystCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Analyst/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAnalyst.html', controller: 'saveAnalystCtrl', caseInsensitiveMatch: true })

            //Manager
            .when('/:moduleName/Manager/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableManager.html', controller: 'listTableManagerCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Manager/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailManager.html', controller: 'detailManagerCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Manager/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveManager.html', controller: 'saveManagerCtrl', caseInsensitiveMatch: true })

			//StrategyGroup
            .when('/:moduleName/StrategyGroup/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableStrategyGroup.html', controller: 'listTableStrategyGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/StrategyGroup/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailStrategyGroup.html', controller: 'detailStrategyGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/StrategyGroup/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveStrategyGroup.html', controller: 'saveStrategyGroupCtrl', caseInsensitiveMatch: true })

            //RatingServices
            .when('/:moduleName/RatingServices/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableRatingServices.html', controller: 'listTableRatingServicesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/RatingServices/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailRatingServices.html', controller: 'detailRatingServicesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/RatingServices/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveRatingServices.html', controller: 'saveRatingServicesCtrl', caseInsensitiveMatch: true })

            //PortfolioType
            .when('/:moduleName/PortfolioType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTablePortfolioType.html', controller: 'listTablePortfolioTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PortfolioType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailPortfolioType.html', controller: 'detailPortfolioTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PortfolioType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/savePortfolioType.html', controller: 'savePortfolioTypeCtrl', caseInsensitiveMatch: true })

             //CompanyDealType
            .when('/:moduleName/CompanyDealType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCompanyDealType.html', controller: 'listTableCompanyDealTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CompanyDealType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCompanyDealType.html', controller: 'detailCompanyDealTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CompanyDealType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCompanyDealType.html', controller: 'saveCompanyDealTypeCtrl', caseInsensitiveMatch: true })

            //Sector
            .when('/:moduleName/Sector/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSector.html', controller: 'listTableSectorCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Sector/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSector.html', controller: 'detailSectorCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Sector/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSector.html', controller: 'saveSectorCtrl', caseInsensitiveMatch: true })

            //Rating
            .when('/:moduleName/Rating/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableRating.html', controller: 'listTableRatingCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Rating/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailRating.html', controller: 'detailRatingCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Rating/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveRating.html', controller: 'saveRatingCtrl', caseInsensitiveMatch: true })

            //TransactionEvent 
            .when('/:moduleName/TransactionEvent/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTransactionEvent.html', controller: 'listTableTransactionEventCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEvent/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTransactionEvent.html', controller: 'detailTransactionEventCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEvent/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTransactionEvent.html', controller: 'saveTransactionEventCtrl', caseInsensitiveMatch: true })

             //TransactionEventBuy 
            .when('/:moduleName/TransactionEventBuy/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTransactionEventBuy.html', controller: 'listTableTransactionEventBuyCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEventBuy/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTransactionEventBuy.html', controller: 'detailTransactionEventBuyCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEventBuy/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTransactionEventBuy.html', controller: 'saveTransactionEventBuyCtrl', caseInsensitiveMatch: true })

            //TransactionEventSell
            .when('/:moduleName/TransactionEventSell/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTransactionEventSell.html', controller: 'listTableTransactionEventSellCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEventSell/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTransactionEventSell.html', controller: 'detailTransactionEventSellCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEventSell/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTransactionEventSell.html', controller: 'saveTransactionEventSellCtrl', caseInsensitiveMatch: true })

            //TransactionEventSellShort
            .when('/:moduleName/TransactionEventSellShort/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTransactionEventSellShort.html', controller: 'listTableTransactionEventSellShortCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEventSellShort/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTransactionEventSellShort.html', controller: 'detailTransactionEventSellShortCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEventSellShort/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTransactionEventSellShort.html', controller: 'saveTransactionEventSellShortCtrl', caseInsensitiveMatch: true })

            //TransactionEventCoverShort
            .when('/:moduleName/TransactionEventCoverShort/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTransactionEventCoverShort.html', controller: 'listTableTransactionEventCoverShortCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEventCoverShort/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTransactionEventCoverShort.html', controller: 'detailTransactionEventCoverShortCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionEventCoverShort/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTransactionEventCoverShort.html', controller: 'saveTransactionEventCoverShortCtrl', caseInsensitiveMatch: true })

            //TxTradeAndSettleDates
            .when('/:moduleName/TxTradeAndSettleDates/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTxTradeAndSettleDates.html', controller: 'listTableTxTradeAndSettleDatesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxTradeAndSettleDates/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTxTradeAndSettleDates.html', controller: 'detailTxTradeAndSettleDatesCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxTradeAndSettleDates/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTxTradeAndSettleDates.html', controller: 'saveTxTradeAndSettleDatesCtrl', caseInsensitiveMatch: true })

             //TxTradeInfo
            .when('/:moduleName/TxTradeInfo/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTxTradeInfo.html', controller: 'listTableTxTradeInfoCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxTradeInfo/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTxTradeInfo.html', controller: 'detailTxTradeInfoCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxTradeInfo/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTxTradeInfo.html', controller: 'saveTxTradeInfoCtrl', caseInsensitiveMatch: true })


            //TxTradeFigures
            .when('/:moduleName/TxTradeFigures/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTxTradeFigures.html', controller: 'listTableTxTradeFiguresCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxTradeFigures/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTxTradeFigures.html', controller: 'detailTxTradeFiguresCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxTradeFigures/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTxTradeFigures.html', controller: 'saveTxTradeFiguresCtrl', caseInsensitiveMatch: true })

             //TxSettlementInfo 
            .when('/:moduleName/TxSettlementInfo/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTxSettlementInfo.html', controller: 'listTableTxSettlementInfoCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxSettlementInfo/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTxSettlementInfo.html', controller: 'detailTxSettlementInfoCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxSettlementInfo/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTxSettlementInfo.html', controller: 'saveTxSettlementInfoCtrl', caseInsensitiveMatch: true })

            //TxInvestment
            .when('/:moduleName/TxInvestment/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTxInvestment.html', controller: 'listTableTxInvestmentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxInvestment/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTxInvestment.html', controller: 'detailTxInvestmentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxInvestment/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTxInvestment.html', controller: 'saveTxInvestmentCtrl', caseInsensitiveMatch: true })

             //TxOther 
            .when('/:moduleName/TxOther/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTxOther.html', controller: 'listTableTxOtherCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxOther/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTxOther.html', controller: 'detailTxOtherCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TxOther/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTxOther.html', controller: 'saveTxOtherCtrl', caseInsensitiveMatch: true })

            //FinancialAccountType 
            .when('/:moduleName/FinancialAccountType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableFinancialAccountType.html', controller: 'listTableFinancialAccountTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FinancialAccountType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailFinancialAccountType.html', controller: 'detailFinancialAccountTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FinancialAccountType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveFinancialAccountType.html', controller: 'saveFinancialAccountTypeCtrl', caseInsensitiveMatch: true })

             //FinancialAccountClass
            .when('/:moduleName/FinancialAccountClass/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableFinancialAccountClass.html', controller: 'listTableFinancialAccountClassCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FinancialAccountClass/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailFinancialAccountClass.html', controller: 'detailFinancialAccountClassCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FinancialAccountClass/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveFinancialAccountClass.html', controller: 'saveFinancialAccountClassCtrl', caseInsensitiveMatch: true })

             //CreditDeal
            .when('/:moduleName/CreditDeal/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCreditDeal.html', controller: 'listTableCreditDealCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditDeal/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCreditDeal.html', controller: 'detailCreditDealCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditDeal/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCreditDeal.html', controller: 'saveCreditDealCtrl', caseInsensitiveMatch: true })

             //CreditFacility
            .when('/:moduleName/CreditFacility/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCreditFacility.html', controller: 'listTableCreditFacilityCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditFacility/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCreditFacility.html', controller: 'detailCreditFacilityCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditFacility/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCreditFacility.html', controller: 'saveCreditFacilityCtrl', caseInsensitiveMatch: true })

             //CreditContract
            .when('/:moduleName/CreditContract/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCreditContract.html', controller: 'listTableCreditContractCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditContract/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCreditContract.html', controller: 'detailCreditContractCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CreditContract/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCreditContract.html', controller: 'saveCreditContractCtrl', caseInsensitiveMatch: true })

             //InvestmentTheme
            .when('/:moduleName/InvestmentTheme/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableInvestmentTheme.html', controller: 'listTableInvestmentThemeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/InvestmentTheme/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailInvestmentTheme.html', controller: 'detailInvestmentThemeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/InvestmentTheme/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveInvestmentTheme.html', controller: 'saveInvestmentThemeCtrl', caseInsensitiveMatch: true })


             //Industry
            .when('/:moduleName/Industry/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableIndustry.html', controller: 'listTableIndustryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Industry/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailIndustry.html', controller: 'detailIndustryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Industry/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveIndustry.html', controller: 'saveIndustryCtrl', caseInsensitiveMatch: true })

             //SubIndustry
            .when('/:moduleName/SubIndustry/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSubIndustry.html', controller: 'listTableSubIndustryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SubIndustry/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSubIndustry.html', controller: 'detailSubIndustryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SubIndustry/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSubIndustry.html', controller: 'saveSubIndustryCtrl', caseInsensitiveMatch: true })

            //SubBusinessUnit
            .when('/:moduleName/SubBusinessUnit/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSubBusinessUnit.html', controller: 'listTableSubBusinessUnitCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SubBusinessUnit/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSubBusinessUnit.html', controller: 'detailSubBusinessUnitCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SubBusinessUnit/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSubBusinessUnit.html', controller: 'saveSubBusinessUnitCtrl', caseInsensitiveMatch: true })


            //BusinessUnit
            .when('/:moduleName/BusinessUnit/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableBusinessUnit.html', controller: 'listTableBusinessUnitCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BusinessUnit/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailBusinessUnit.html', controller: 'detailBusinessUnitCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BusinessUnit/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveBusinessUnit.html', controller: 'saveBusinessUnitCtrl', caseInsensitiveMatch: true })

              //BrokerGroup
            .when('/:moduleName/BrokerGroup/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableBrokerGroup.html', controller: 'listTableBrokerGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BrokerGroup/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailBrokerGroup.html', controller: 'detailBrokerGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BrokerGroup/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveBrokerGroup.html', controller: 'saveBrokerGroupCtrl', caseInsensitiveMatch: true })

              //BrokerType
            .when('/:moduleName/BrokerType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableBrokerType.html', controller: 'listTableBrokerTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BrokerType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailBrokerType.html', controller: 'detailBrokerTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BrokerType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveBrokerType.html', controller: 'saveBrokerTypeCtrl', caseInsensitiveMatch: true })

            //Security
            .when('/:moduleName/Security/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurity.html', controller: 'listTableSecurityCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Security/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurity.html', controller: 'detailSecurityCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Security/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurity.html', controller: 'saveSecurityCtrl', caseInsensitiveMatch: true })

            //SecurityType
            .when('/:moduleName/SecurityType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurityType.html', controller: 'listTableSecurityTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurityType.html', controller: 'detailSecurityTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurityType.html', controller: 'saveSecurityTypeCtrl', caseInsensitiveMatch: true })

            //SecurityTypeGroup
            .when('/:moduleName/SecurityTypeGroup/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurityTypeGroup.html', controller: 'listTableSecurityTypeGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityTypeGroup/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurityTypeGroup.html', controller: 'detailSecurityTypeGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityTypeGroup/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurityTypeGroup.html', controller: 'saveSecurityTypeGroupCtrl', caseInsensitiveMatch: true })

             //AdjustmentCategory
            .when('/:moduleName/AdjustmentCategory/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableAdjustmentCategory.html', controller: 'listTableAdjustmentCategoryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AdjustmentCategory/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailAdjustmentCategory.html', controller: 'detailAdjustmentCategoryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AdjustmentCategory/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveAdjustmentCategory.html', controller: 'saveAdjustmentCategoryCtrl', caseInsensitiveMatch: true })


             //RecordType
            .when('/:moduleName/RecordType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableRecordType.html', controller: 'listTableRecordTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/RecordType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailRecordType.html', controller: 'detailRecordTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/RecordType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveRecordType.html', controller: 'saveRecordTypeCtrl', caseInsensitiveMatch: true })

             //SecurityXExternalMarketDataIdentifier
            .when('/:moduleName/SecurityXExternalMarketDataIdentifier/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurityXExternalMarketDataIdentifier.html', controller: 'listTableSecurityXExternalMarketDataIdentifierCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXExternalMarketDataIdentifier/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurityXExternalMarketDataIdentifier.html', controller: 'detailSecurityXExternalMarketDataIdentifierCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXExternalMarketDataIdentifier/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurityXExternalMarketDataIdentifier.html', controller: 'saveSecurityXExternalMarketDataIdentifierCtrl', caseInsensitiveMatch: true })

             //SecurityXOther
            .when('/:moduleName/SecurityXOther/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurityXOther.html', controller: 'listTableSecurityXOtherCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXOther/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurityXOther.html', controller: 'detailSecurityXOtherCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXOther/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurityXOther.html', controller: 'saveSecurityXOtherCtrl', caseInsensitiveMatch: true })

             //SecurityXSettlementDay
            .when('/:moduleName/SecurityXSettlementDay/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurityXSettlementDay.html', controller: 'listTableSecurityXSettlementDayCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXSettlementDay/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurityXSettlementDay.html', controller: 'detailSecurityXSettlementDayCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXSettlementDay/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurityXSettlementDay.html', controller: 'saveSecurityXSettlementDayCtrl', caseInsensitiveMatch: true })

            //SecurityXParty
            .when('/:moduleName/SecurityXParty/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurityXParty.html', controller: 'listTableSecurityXPartyCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXParty/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurityXParty.html', controller: 'detailSecurityXPartyCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXParty/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurityXParty.html', controller: 'saveSecurityXPartyCtrl', caseInsensitiveMatch: true })

            //SecurityXInvestmentIdentifier
            .when('/:moduleName/SecurityXInvestmentIdentifier/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableSecurityXInvestmentIdentifier.html', controller: 'listTableSecurityXInvestmentIdentifierCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXInvestmentIdentifier/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailSecurityXInvestmentIdentifier.html', controller: 'detailSecurityXInvestmentIdentifierCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SecurityXInvestmentIdentifier/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveSecurityXInvestmentIdentifier.html', controller: 'saveSecurityXInvestmentIdentifierCtrl', caseInsensitiveMatch: true })
            
            //MarketCapitalizationCategory
            .when('/:moduleName/MarketCapitalizationCategory/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableMarketCapitalizationCategory.html', controller: 'listTableMarketCapitalizationCategoryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MarketCapitalizationCategory/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailMarketCapitalizationCategory.html', controller: 'detailMarketCapitalizationCategoryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MarketCapitalizationCategory/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveMarketCapitalizationCategory.html', controller: 'saveMarketCapitalizationCategoryCtrl', caseInsensitiveMatch: true })
        
            //OrderAction
            .when('/:moduleName/OrderAction/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableOrderAction.html', controller: 'listTableOrderActionCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderAction/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailOrderAction.html', controller: 'detailOrderActionCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderAction/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveOrderAction.html', controller: 'saveOrderActionCtrl', caseInsensitiveMatch: true })

            //OrderRequest
            .when('/:moduleName/OrderRequest/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableOrderRequest.html', controller: 'listTableOrderRequestCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderRequest/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailOrderRequest.html', controller: 'detailOrderRequestCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderRequest/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveOrderRequest.html', controller: 'saveOrderRequestCtrl', caseInsensitiveMatch: true })

            //OrderType
            .when('/:moduleName/OrderType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableOrderType.html', controller: 'listTableOrderTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailOrderType.html', controller: 'detailOrderTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveOrderType.html', controller: 'saveOrderTypeCtrl', caseInsensitiveMatch: true })

            //OrderStatusGroup
            .when('/:moduleName/OrderStatusGroup/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableOrderStatusGroup.html', controller: 'listTableOrderStatusGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderStatusGroup/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailOrderStatusGroup.html', controller: 'detailOrderStatusGroupCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderStatusGroup/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveOrderStatusGroup.html', controller: 'saveOrderStatusGroupCtrl', caseInsensitiveMatch: true })
                        
            //OrderStatus
            .when('/:moduleName/OrderStatus/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableOrderStatus.html', controller: 'listTableOrderStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderStatus/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailOrderStatus.html', controller: 'detailOrderStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderStatus/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveOrderStatus.html', controller: 'saveOrderStatusCtrl', caseInsensitiveMatch: true })

            //OrderStatusType
            .when('/:moduleName/OrderStatusType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableOrderStatusType.html', controller: 'listTableOrderStatusTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderStatusType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailOrderStatusType.html', controller: 'detailOrderStatusTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderStatusType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveOrderStatusType.html', controller: 'saveOrderStatusTypeCtrl', caseInsensitiveMatch: true })

            //OrderItem 
            .when('/:moduleName/OrderItem/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableOrderItem.html', controller: 'listTableOrderItemCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderItem/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailOrderItem.html', controller: 'detailOrderItemCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/OrderItem/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveOrderItem.html', controller: 'saveOrderItemCtrl', caseInsensitiveMatch: true })

            //CommissionCode
            .when('/:moduleName/CommissionCode/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCommissionCode.html', controller: 'listTableCommissionCodeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CommissionCode/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCommissionCode.html', controller: 'detailCommissionCodeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CommissionCode/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCommissionCode.html', controller: 'saveCommissionCodeCtrl', caseInsensitiveMatch: true })

            //CommissionRate
            .when('/:moduleName/CommissionRate/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCommissionRate.html', controller: 'listTableCommissionRateCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CommissionRate/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCommissionRate.html', controller: 'detailCommissionRateCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CommissionRate/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCommissionRate.html', controller: 'saveCommissionRateCtrl', caseInsensitiveMatch: true })

            //CommissionType
            .when('/:moduleName/CommissionType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCommissionType.html', controller: 'listTableCommissionTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CommissionType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCommissionType.html', controller: 'detailCommissionTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CommissionType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCommissionType.html', controller: 'saveCommissionTypeCtrl', caseInsensitiveMatch: true })
            
            //CommissionSplit
            .when('/:moduleName/CommissionSplit/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableCommissionSplit.html', controller: 'listTableCommissionSplitCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CommissionSplit/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailCommissionSplit.html', controller: 'detailCommissionSplitCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CommissionSplit/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveCommissionSplit.html', controller: 'saveCommissionSplitCtrl', caseInsensitiveMatch: true })
            
            //MSPAFile
            .when('/:moduleName/MSPAFile/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableMSPAFile.html', controller: 'listTableMSPAFileCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MSPAFile/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailMSPAFile.html', controller: 'detailMSPAFileCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MSPAFile/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveMSPAFile.html', controller: 'saveMSPAFileCtrl', caseInsensitiveMatch: true })

            //MSPAFileEvent
            .when('/:moduleName/MSPAFileEvent/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableMSPAFileEvent.html', controller: 'listTableMSPAFileEventCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MSPAFileEvent/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailMSPAFileEvent.html', controller: 'detailMSPAFileEventCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MSPAFileEvent/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveMSPAFileEvent.html', controller: 'saveMSPAFileEventCtrl', caseInsensitiveMatch: true })

            //TradingEventType
            .when('/:moduleName/TradingEventType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTradingEventType.html', controller: 'listTableTradingEventTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TradingEventType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTradingEventType.html', controller: 'detailTradingEventTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TradingEventType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTradingEventType.html', controller: 'saveTradingEventTypeCtrl', caseInsensitiveMatch: true })

            //MSPAFileEventType
            .when('/:moduleName/MSPAFileEventType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableMSPAFileEventType.html', controller: 'listTableMSPAFileEventTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MSPAFileEventType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailMSPAFileEventType.html', controller: 'detailMSPAFileEventTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MSPAFileEventType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveMSPAFileEventType.html', controller: 'saveMSPAFileEventTypeCtrl', caseInsensitiveMatch: true })

            // help
            .when('/help', { templateUrl: '../../app/help/views/help/help.html' })
            .otherwise({ redirectTo: '/main' });

        //REQUIRED - capture all 401 http response errors
        $httpProvider.interceptors.push(function ($rootScope, $q) {
            return {
                'responseError': function (rejection) {
                    if (rejection.status == 401) {
                        var deferred = $q.defer();
                        $rootScope.$broadcast('auth-loginRequired');
                        return deferred.promise;
                    } else {
                        return $q.reject(rejection);
                    }
                }
            };
        }
    );

        //REQUIRED - disable ajax request caching. We do not want the browser to cache any results, we want new data every time we request it
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    })
    .run(function ($rootScope, $route, $location, userService) {

        $rootScope.ShowDetailedSupportInfo = true;

        $rootScope.$on('$routeChangeStart', function () {
            if ($location.path() != '/login') {
                userService.setPreviousRoute(userService.getNextRoute().path);
                userService.setNextRoute($location.path(), 'set next route');
            }
        });

        //REQUIRED - redirect 401 response errors to login
        $rootScope.$on('auth-loginRequired', function () {
            userService.setNextError('You must login to access this page');
            $location.url('login');
        });


        $rootScope.$on('$routeChangeSuccess', function (event, currentRoute, previousRoute) {

            $rootScope.title = $route.current.title;
            
        });

    })
    .directive('hboTabs', function () {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs) {
                var jqueryElm = $(elm[0]);
                $(jqueryElm).tabs({
                    activate: function (event, ui) {

                        //get activated tab
                        var procType = ui.newPanel.attr("title");

                        //refreshes ace editor w.o needing to put the cursor
                        ace.edit("editor" + procType + "Procedure").resize();
                    }
                });
            }
        };
    })
    .directive('jqdatepicker', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ctrl) {
                $(element).datepicker({
                    dateFormat: 'dd.mm.yy',
                    onSelect: function (date) {
                        ctrl.$setViewValue(date);
                        ctrl.$render();
                        scope.$apply();
                    }
                });
            }
        };
    })
    .directive('navMenu', ['$parse', '$compile', function ($parse, $compile) {
        return {
            restrict: 'C', //Element
            scope: true,
            link: function (scope, element, attrs) {
                scope.selectedNode = null;

                scope.$watch(attrs.menuData, function (val) {

                    var template = angular.element('<ul class="nav navbar-nav "><li class="dropdown" ng-repeat="node in ' + attrs.menuData + '"><a ng-if="node.children.length>0" class="dropdown-toggle" data-toggle="dropdown" href="{{node.href}}">{{node.text}}<span class="caret"></span></a><a ng-if="node.children.length==0" href="{{node.href}}" >{{node.text}}</a><sub-navigation-tree></sub-navigation-tree></li></ul>');

                    var linkFunction = $compile(template);
                    linkFunction(scope);
                    element.html(null).append(template);

                }, true);
            }
        };
    }])
    .directive('subNavigationTree', ['$compile', function ($compile) {
        return {
            restrict: 'E', //Element
            scope: true,
            link: function (scope, element, attrs) {
                scope.tree = scope.node;

                if (scope.tree.children && scope.tree.children.length) {
                    var template = angular.element('<ul class="dropdown-menu"><li class="dropdown" ng-repeat="node in tree.children" ng-class="{\'dropdown\' : node.children.length, \'dropdown-submenu\': node.children.length}"><a href="{{node.href}}">{{node.text}}</a><sub-navigation-tree  tree="node"></sub-navigation-tree></li></ul>');

                    var linkFunction = $compile(template);
                    linkFunction(scope);
                    element.replaceWith(template);
                }
                else {
                    element.remove();
                }
            }
        };
    }])
    .factory('autoCompleteDataService', function () {
        return {
            methodName: 'GetAutoCompleteList',
            getAutoCompleteMethod: function (columnName) {
                var objInfo = {};
                if (columnName == 'FundId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'FundId';
                    objInfo["MethodName"] = 'GetFundList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Fund';
                    objInfo["ModuleName"] = 'SecurityMaster';
                }
                else if (columnName == 'ManagementFirmId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'ManagementFirmId';
                    objInfo["MethodName"] = 'GetManagementFirmList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'ManagementFirm';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'BrokerId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'BrokerId';
                    objInfo["MethodName"] = 'GetBrokerList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Broker';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'ExchangeId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'ExchangeId';
                    objInfo["MethodName"] = 'GetExchangeList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Exchange';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'TradingEventTypeId') {                   
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'TradingEventTypeId';
                    objInfo["MethodName"] = 'GetTradingEventTypeList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'TradingEventType';
                    objInfo["ModuleName"] = 'Integration';
                }
                else if (columnName == 'PortfolioId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'PortfolioId';
                    objInfo["MethodName"] = 'GetPortfolioList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Portfolio';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'TransactionTypeId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'TransactionTypeId';
                    objInfo["MethodName"] = 'GetTransactionTypeList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'TransactionType';
                    objInfo["ModuleName"] = 'Transactions';
                }
                else if (columnName == 'CustodianId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'CustodianId';
                    objInfo["MethodName"] = 'GetCustodianList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Custodian';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'StrategyId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'StrategyId';
                    objInfo["MethodName"] = 'GetStrategyList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Strategy';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'AccountSpecificTypeId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'AccountSpecificTypeId';
                    objInfo["MethodName"] = 'GetAccountSpecificTypeList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'AccountSpecificType';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'InvestmentTypeId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'InvestmentTypeId';
                    objInfo["MethodName"] = 'GetInvestmentTypeList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'InvestmentType';
                    objInfo["ModuleName"] = 'SecurityMaster';
                }
                else {
                    objInfo["Found"] = false;
                }
                return objInfo;
            }
        };
    });

Date.prototype.chromeDate = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return yyyy + "-" + (mm[1] ? mm : "0" + mm[0]) + "-" + (dd[1] ? dd : "0" + dd[0]); // padding
};
