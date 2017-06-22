/******************************************************************************
**		Name: AccidentReport
*******************************************************************************/

EXEC dbo.AccidentReport_Update @AccidentReportId = 23	,	@StudentId = 26    ,	@Data = '12/15/2011'	,	@AccidentPlaceId = 29  ,  @AccidentPlace = 'RailWay'     ,  @TeacherId = 32  , @Description = 'Rail'   , @Remedy = 'LeftHandInjured'    , @SignOffParent = 'Shell'  , @SignOffTeacher = 'Fania'  , @SignOffAdmin = 'Kania'  
EXEC dbo.AccidentReport_Update @AccidentReportId = 24	,	@StudentId = 27	   ,	@Data = '12/16/2011'	,   @AccidentPlaceId = 30  ,  @AccidentPlace = 'AirPort'     ,  @TeacherId = 33  , @Description = 'Car'    , @Remedy = 'FracturedHands'     , @SignOffParent = 'Sain'   , @SignOffTeacher = 'Jania'  , @SignOffAdmin = 'Josia'
EXEC dbo.AccidentReport_Update @AccidentReportId = 25	,	@StudentId = 28	   ,	@Data = '12/17/2011'	,	@AccidentPlaceId = 31  ,  @AccidentPlace = 'MainHall'    ,  @TeacherId = 34  , @Description = 'Truck'  , @Remedy = 'InjuredHand'        , @SignOffParent = 'Gain'   , @SignOffTeacher = 'Nania'  , @SignOffAdmin = 'Losia'

