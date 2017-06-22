IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonTitleClone')
BEGIN
	PRINT 'Dropping Procedure PersonTitleClone'
	DROP  Procedure PersonTitleClone
END
GO

PRINT 'Creating Procedure PersonTitleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: PersonTitleClone
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.PersonTitleClone
(
		@PersonTitleId		INT			= NULL 	OUTPUT		
	,	@Name				VARCHAR(50)						
	,	@Description		VARCHAR(50)						
	,	@SortOrder			INT								
	,	@AuditId			INT									
	,	@AuditDate			DATETIME	= NULL				
	,	@SystemEntityType	VARCHAR(50)	= 'PersonTitle'					
)
AS
BEGIN		
	
	SELECT	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.PersonTitle
	WHERE   PersonTitleId		= @PersonTitleId

	EXEC dbo.PersonTitleInsert 
			@PersonTitleId	=	NULL
		,	@Name			=	@Name
		,	@Description	=	@Description
		,	@SortOrder		=	@SortOrder
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PersonTitleId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
