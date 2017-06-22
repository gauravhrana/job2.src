IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='DiaperStatusDoesExist')
BEGIN
	PRINT 'Dropping Procedure DiaperStatusDoesExist'
	DROP  Procedure  DiaperStatusDoesExist
END
GO

PRINT 'Creating Procedure DiaperStatusDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: DiaperStatusDoesExist
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

Create procedure dbo.DiaperStatusDoesExist
(
		@Name					VARCHAR(50)		= NULL	
	,	@ApplicationId			INT	
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)  	= 'DiaperStatus'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.DiaperStatus a
	WHERE		a.Name = @Name
	AND			a.ApplicationId	= ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

