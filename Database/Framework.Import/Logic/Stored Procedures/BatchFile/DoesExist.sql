IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='BatchFileDoesExist')
BEGIN
	PRINT 'Dropping Procedure BatchFileDoesExist'
	DROP  Procedure  BatchFileDoesExist
END
GO

PRINT 'Creating Procedure BatchFileDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileDoesExist
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

Create procedure dbo.BatchFileDoesExist
(
		@Name					VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFile'	
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.BatchFile a
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

