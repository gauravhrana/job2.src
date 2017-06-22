IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemGetMaxID')
BEGIN
	PRINT 'Dropping Procedure SystemGetMaxID'
	DROP PROCEDURE SystemGetMaxID
END
GO

PRINT 'Creating Procedure SystemGetMaxID'
GO

/******************************************************************************
**		File: 
**		Name: SystemGetMaxID
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE PROCEDURE dbo.SystemGetMaxID


AS
BEGIN        
 
  
    SELECT MAX(AccidentPlaceId) MAXID, 'AccidentPlace' TABLENAME  FROM dbo.AccidentPlace
	UNION
	SELECT MAX(AccidentReportId) MAXID, 'AccidentReport' TABLENAME    FROM dbo.AccidentReport
	UNION
	SELECT MAX(ActivityId) MAXID, 'Activity' TABLENAME   FROM dbo.Activity
	UNION
	SELECT MAX(ActivitySubTypeId) MAXID, 'ActivitySubType' TABLENAME   FROM dbo.ActivitySubType
	UNION
	SELECT MAX(ActivityTypeId) MAXID, 'ActivityType' TABLENAME   FROM dbo.ActivityType
	UNION
	SELECT MAX(BathroomId) MAXID, 'Bathroom' TABLENAME   FROM dbo.Bathroom
	UNION
	SELECT MAX(CommentId) MAXID, 'Comment' TABLENAME   FROM dbo.Comment
	UNION
	SELECT MAX(DiaperStatusId) MAXID, 'DiaperStatus' TABLENAME   FROM dbo.DiaperStatus
	UNION
	SELECT MAX(DiscountId) MAXID, 'Discount' TABLENAME   FROM dbo.Discount
	UNION
	SELECT MAX(EventTypeId) MAXID, 'EventType' TABLENAME   FROM dbo.EventType
	UNION
	SELECT MAX(FoodTypeId) MAXID, 'FoodType' TABLENAME   FROM dbo.FoodType
	UNION
	SELECT MAX(MealId) MAXID, 'Meal' TABLENAME   FROM dbo.Meal
	UNION
	SELECT MAX(MealDetailId) MAXID, 'MealDetail' TABLENAME   FROM dbo.MealDetail
	UNION
	SELECT MAX(MealTypeId) MAXID, 'MealType' TABLENAME   FROM dbo.MealType
	UNION
	SELECT MAX(NeedItemId) MAXID, 'NeedItem' TABLENAME   FROM dbo.NeedItem
	UNION
	SELECT MAX(NeedsId) MAXID, 'Needs' TABLENAME   FROM dbo.Needs
	UNION
	SELECT MAX(PaymentMethodId) MAXID, 'PaymentMethod' TABLENAME   FROM dbo.PaymentMethod
	UNION
	SELECT MAX(SickReportId) MAXID, 'SickReport' TABLENAME   FROM dbo.SickReport
	UNION
	SELECT MAX(SleepId) MAXID, 'Sleep' TABLENAME   FROM dbo.Sleep
	UNION
	SELECT MAX(StudentId) MAXID, 'Needs' TABLENAME   FROM dbo.Student
	UNION
	SELECT MAX(TeacherId) MAXID, 'Teacher' TABLENAME   FROM dbo.Teacher
	UNION
	SELECT MAX(TuitionId) MAXID, 'Sleep' TABLENAME   FROM dbo.Tuition


END