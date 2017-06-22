IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ActivityDoesExist')
BEGIN
	PRINT 'Dropping Procedure ActivityDoesExist'
	DROP  Procedure  ActivityDoesExist
END
GO

PRINT 'Creating Procedure ActivityDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ActivityDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.ActivityDoesExist
(
		@StudentId				INT				= NULL	
	,	@ApplicationId			INT						
	,	@ActivityTypeId			INT				= NULL		
	,	@ActivitySubTypeId		INT				= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Activity'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.Activity a
	WHERE		a.StudentId			=	@StudentId	
	AND			a.ActivityTypeId	=	@ActivityTypeId
	AND			a.ActivitySubTypeId	=	@ActivitySubTypeId	
	AND			a.ApplicationId		=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

