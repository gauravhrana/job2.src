/******************************************************************************
**		Name: SickReport
*******************************************************************************/

EXEC dbo.SickReport_Insert @SickReportId = -12	,	@StudentId = 114,	@TypeofSickness = 'Fever',	@AmountofSickness = 'High',   @FreqofSickness = 'Weekly',   @TeacherSickNote = 'Weekly',		@ReturnToSchoolDate = '1/5/2012'
EXEC dbo.SickReport_Insert @SickReportId = -28	,	@StudentId = 141,	@TypeofSickness = 'Clod',	@AmountofSickness = 'Low',    @FreqofSickness = 'Monthly',  @TeacherSickNote = 'Quaterly',		@ReturnToSchoolDate = '1/6/2012'
EXEC dbo.SickReport_Insert @SickReportId = -19	,	@StudentId = 116,	@TypeofSickness = 'Cuff',	@AmountofSickness = 'Medium', @FreqofSickness = 'Yearly',   @TeacherSickNote = 'Daily',		    @ReturnToSchoolDate = '1/7/2012'

