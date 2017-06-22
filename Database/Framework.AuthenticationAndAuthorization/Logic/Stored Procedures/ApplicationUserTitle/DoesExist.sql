IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleDoesExist'
	DROP  Procedure  ApplicationUserTitleDoesExist
END
GO

PRINT 'Creating Procedure ApplicationUserTitleDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserTitleDoesExist
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
CREATE Procedure dbo.ApplicationUserTitleDoesExist
(	
		@Name				VARCHAR(50)		= NULL		
	,	@ApplicationUserTitleId			INT				= NULL	
	,	@AuditId			INT							
	,	@AuditDate			DATETIME		= NULL		
	,	@SystemEntityType	VARCHAR(50)		= 'ApplicationUserTitle'			
)
AS
BEGIN	
		
	SELECT	*
	FROM	dbo.ApplicationUserTitle
	WHERE	Name			= @Name

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType					= @SystemEntityType  
		,	@EntityKey							= NULL
		,	@AuditAction						= 'DoesExist' 
		,	@CreatedDate						= @AuditDate
		,	@CreatedByApplicationUserTitleId	= @AuditId
END
GO
