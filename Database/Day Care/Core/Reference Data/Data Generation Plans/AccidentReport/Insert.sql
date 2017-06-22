/******************************************************************************
**		Name: AccidentReport
*******************************************************************************/

EXEC dbo.AccidentReport_Insert @AccidentReportId = 14	,	@StudentId = 17    ,	@Data = '12/12/2011'	,	@AccidentPlaceId = 17  ,  @AccidentPlace = 'Market'     ,  @TeacherId = 20  , @Description = 'Car'   , @Remedy = 'HandInjured'   , @SignOffParent = 'Ran'  , @SignOffTeacher = 'Fani'  , @SignOffAdmin = 'Kani'  
EXEC dbo.AccidentReport_Insert @AccidentReportId = 15	,	@StudentId = 18	   ,	@Data = '12/13/2011'	,   @AccidentPlaceId = 18  ,  @AccidentPlace = 'Road'       ,  @TeacherId = 21  , @Description = 'Bus'   , @Remedy = 'FracturedLeg'  , @SignOffParent = 'San'  , @SignOffTeacher = 'Jani'  , @SignOffAdmin = 'Josi'
EXEC dbo.AccidentReport_Insert @AccidentReportId = 16	,	@StudentId = 19	   ,	@Data = '12/14/2011'	,	@AccidentPlaceId = 19  ,  @AccidentPlace = 'HighWay'    ,  @TeacherId = 22  , @Description = 'Bike'  , @Remedy = 'InjuredHead'   , @SignOffParent = 'Gan'  , @SignOffTeacher = 'Nani'  , @SignOffAdmin = 'Losi'

