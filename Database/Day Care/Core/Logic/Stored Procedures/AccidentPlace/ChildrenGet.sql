IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentPlaceChildrenGet')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceChildrenGet'
	DROP  Procedure AccidentPlaceChildrenGet
END
GO

PRINT 'Creating Procedure AccidentPlaceChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: AccidentPlaceChildrenGet
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

CREATE Procedure dbo.AccidentPlaceChildrenGet
(
		@AccidentPlaceId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'AccidentPlace'
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
		,	b.Name					AS 'AccidentPlace'		
	FROM		dbo.AccidentReport	a
	INNER JOIN	dbo.AccidentPlace	b ON a.AccidentPlaceId	= b.AccidentPlaceId	
	WHERE		a.AccidentPlaceId = @AccidentPlaceId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AccidentPlaceId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   