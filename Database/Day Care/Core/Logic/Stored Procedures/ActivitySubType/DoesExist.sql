IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ActivitySubTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeDoesExist'
	DROP  Procedure  ActivitySubTypeDoesExist
END
GO

PRINT 'Creating Procedure ActivitySubTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ActivitySubTypeDoesExist
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

Create procedure dbo.ActivitySubTypeDoesExist
(
		@Name					VARCHAR(50)		= NULL	
	,	@ApplicationId			INT	
	,	@ActivityTypeId			INT				= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'ActivitySubType'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.ActivitySubType a
	WHERE		a.Name				=	@Name	
	AND			a.ActivityTypeId	=	@ActivityTypeId
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

