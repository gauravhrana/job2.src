IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='NeedItemDoesExist')
BEGIN
	PRINT 'Dropping Procedure NeedItemDoesExist'
	DROP  Procedure  NeedItemDoesExist
END
GO

PRINT 'Creating Procedure NeedItemDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: NeedItemDoesExist
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

Create procedure dbo.NeedItemDoesExist
(
		@ApplicationId			INT					
	,	@Name					VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'NeedItem'				
	
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.NeedItem a
	WHERE		a.Name			= @Name	
			AND	a.ApplicationId = @ApplicationId


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

