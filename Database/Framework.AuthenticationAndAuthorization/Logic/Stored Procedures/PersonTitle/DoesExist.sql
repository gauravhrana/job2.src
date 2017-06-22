IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonTitleDoesExist')
BEGIN
	PRINT 'Dropping Procedure PersonTitleDoesExist'
	DROP  Procedure  PersonTitleDoesExist
END
GO

PRINT 'Creating Procedure PersonTitleDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: PersonTitleDoesExist
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
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
CREATE Procedure dbo.PersonTitleDoesExist
(	
		@NameLast			VARCHAR(50)		= NULL		
	,	@NameFirst			VARCHAR(50)		= NULL	
	,	@PersonTitleId			INT				= NULL	
	,	@AuditId			INT							
	,	@AuditDate			DATETIME		= NULL		
	,	@SystemEntityType	VARCHAR(50)		= 'PersonTitle'			
)
AS
BEGIN	
		
	SELECT	*
	FROM	dbo.PersonTitle
	WHERE	Name			= @Name

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType  
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonTitleId	= @AuditId
END
GO
