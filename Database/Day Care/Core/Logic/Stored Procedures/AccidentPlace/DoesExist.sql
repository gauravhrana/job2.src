IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='AccidentPlaceDoesExist')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceDoesExist'
	DROP  Procedure  AccidentPlaceDoesExist
END
GO

PRINT 'Creating Procedure AccidentPlaceDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: AccidentPlaceDoesExist
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

Create procedure dbo.AccidentPlaceDoesExist
(
		@Name					VARCHAR(50)		= NULL	
	,	@ApplicationId			INT			
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AccidentPlace'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.AccidentPlace a
	WHERE		a.Name = @Name	
	AND			a.ApplicationId	= @ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

