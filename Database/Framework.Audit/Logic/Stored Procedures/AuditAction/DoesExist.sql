IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='AuditActionDoesExist')
BEGIN
	PRINT 'Dropping Procedure AuditActionDoesExist'
	DROP  Procedure  AuditActionDoesExist
END
GO

PRINT 'Creating Procedure AuditActionDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: AuditActionDoesExist
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

Create procedure dbo.AuditActionDoesExist
(
		@Name				VARCHAR(50)		= NULL	
	,	@AuitActionId		INT				= NULL	
	,	@AuditId			INT							
	,	@AuditDate			DATETIME		= NULL		
	,	@SystemEntityType	VARCHAR(50)		= 'AuditAction'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.AuditAction a
	WHERE		a.Name = @Name	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

