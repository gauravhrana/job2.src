IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserDoesExist'
	DROP  Procedure  ApplicationUserDoesExist
END
GO

PRINT 'Creating Procedure ApplicationUserDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserDoesExist
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
CREATE Procedure dbo.ApplicationUserDoesExist
(	
		@NameLast			VARCHAR(50)		= NULL		
	,	@NameFirst			VARCHAR(50)		= NULL	
	,	@ApplicationUserId	INT				= NULL	
	,	@AuditId			INT							
	,	@AuditDate			DATETIME		= NULL		
	,	@SystemEntityType	VARCHAR(50)		= 'ApplicationUser'			
)
AS
BEGIN	
		
	SELECT	*
	FROM	dbo.ApplicationUser
	WHERE	LastName	= @NameLast
	AND		FirstName	= @NameFirst	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType  
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
