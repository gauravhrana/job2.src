IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='BatchFileSetDoesExist')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetDoesExist'
	DROP  Procedure  BatchFileSetDoesExist
END
GO

PRINT 'Creating Procedure BatchFileSetDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileSetDoesExist
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

Create procedure dbo.BatchFileSetDoesExist
(
		@Name					VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFileSet'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.BatchFileSet a
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

