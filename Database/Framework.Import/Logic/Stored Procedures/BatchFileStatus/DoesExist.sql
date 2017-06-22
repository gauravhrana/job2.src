IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='BatchFileStatusDoesExist')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusDoesExist'
	DROP  Procedure  BatchFileStatusDoesExist
END
GO

PRINT 'Creating Procedure BatchFileStatusDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileStatusDoesExist
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

Create procedure dbo.BatchFileStatusDoesExist
(
		@Name					VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFileStatus'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.BatchFileStatus a
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

