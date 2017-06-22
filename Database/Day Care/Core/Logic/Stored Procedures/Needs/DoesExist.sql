IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='NeedsDoesExist')
BEGIN
	PRINT 'Dropping Procedure NeedsDoesExist'
	DROP  Procedure  NeedsDoesExist
END
GO

PRINT 'Creating Procedure NeedsDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: NeedsDoesExist
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

Create procedure dbo.NeedsDoesExist
(
		@StudentId				INT				= NULL		
	,	@ApplicationId			INT				
	,	@RequestDate			DATETIME		= NULL		
	,	@NeedItemId				INT				= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Needs'				
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.Needs a
	WHERE		a.StudentId			=	@StudentId	
	AND			a.RequestDate		=	@RequestDate
	AND			a.NeedItemId		=	@NeedItemId	
	AND			a.ApplicationId		=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Needs'	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

