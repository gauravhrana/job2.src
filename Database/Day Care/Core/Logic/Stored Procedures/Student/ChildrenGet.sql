IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentChildrenGet')
BEGIN
	PRINT 'Dropping Procedure StudentChildrenGet'
	DROP  Procedure StudentChildrenGet
END
GO

PRINT 'Creating Procedure StudentChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: StudentChildrenGet
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.StudentChildrenGet
(
		@StudentId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'Student'
)
AS
BEGIN
--GET AccidentReport Records
	SELECT	a.AccidentReportId
		,	a.ApplicationId
	    ,	a.StudentId	
	    ,	a.Date
	    ,	a.AccidentPlaceId
	    ,	a.TeacherId 
		,	a.Description
		,	a.Remedy
		,	a.SignoffParent
		,	a.SignoffTeacher
		,	a.SignoffAdmin				
	FROM		dbo.AccidentReport	a	
	WHERE		a.StudentId = @StudentId

	--GET Activity Records
	SELECT	a.ActivityId	
		,	a.ApplicationId		
		,	a.StudentId					
		,	a.ActivityTypeId	    
		,	a.ActivitySubTypeId						
	FROM		dbo.Activity a
	WHERE		a.StudentId = @StudentId

	-- GET Bathroom Records
	SELECT  a.BathroomId
		,	a.ApplicationId
		,	a.StudentId
		,	a.TimeIn
		,	a.DiaperStatusId
		,	a.DiaperCream
		,	a.PottyStatus
		,	a.TeacherId
		,	a.TeacherNotes			
	FROM		dbo.Bathroom a
	WHERE		a.StudentId = @StudentId

	--GET Comment Records
	SELECT	a.CommentId
		,	a.ApplicationId
        ,   a.StudentId
        ,	a.Date
		,	a.EventTypeId
		,	a.Comment	
	FROM		dbo.Comment	a
	WHERE		a.StudentId = @StudentId

	--GET Meal Records
	SELECT	a.MealId
		,	a.ApplicationId
		,	a.StudentId
		,	a.Date
		,	a.MealTypeId						
	FROM		dbo.Meal a
	WHERE		a.StudentId = @StudentId

	-- GET Needs Records
	SELECT  a.NeedsId
		,	a.ApplicationId 
        ,   a.StudentId
        ,	a.RequestDate
		,	a.ReceivedDate
		,	a.NeedItemId
		,	a.NeedItemStatus
		,   a.NeedItemBy
	FROM		dbo.Needs a
	WHERE		a.StudentId = @StudentId

	-- GET SickReport Records
	SELECT  a.SickReportId
		,	a.ApplicationId
		,   a.StudentId
		,	a.TypeOfSickness
		,	a.AmountOfSickness
		,	a.FreqOfSickness
		,   a.TeacherSickNote
		,   a.ReturnToSchoolDate
	FROM		dbo.SickReport a	
	WHERE		a.StudentId = @StudentId
	
	-- GET Sleep Records
	SELECT  a.SleepId
		,	a.StudentId
		,	a.Date
		,	a.NapStart
		,	a.NapEnd			
	FROM		dbo.Sleep a	
	WHERE		a.StudentId = @StudentId

	-- GET Tuition Records
	SELECT  a.TuitionId
		,	a.ApplicationId
		,	a.StudentId
		,	a.TuitionDueDate
		,	a.TuitionAmount
		,	a.DiscountId
		,	a.DiscountAmount
		,	a.TuitionAmountDue
		,	a.PaymentMethodId
		,	a.TuitionPaymentAmount
	FROM		dbo.Tuition a
	WHERE		a.StudentId = @StudentId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @StudentId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
