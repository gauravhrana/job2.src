IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonTitleInsert')
BEGIN
	PRINT 'Dropping Procedure PersonTitleInsert'
	DROP  Procedure PersonTitleInsert
END
GO

PRINT 'Creating Procedure PersonTitleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:PersonTitleInsert
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
**********************************************************************************************/

CREATE Procedure dbo.PersonTitleInsert
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

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @PersonTitleId OUTPUT, @AuditId
		
	INSERT INTO dbo.PersonTitle 
	( 
			PersonTitleId						
		,	Name				
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@PersonTitleId		
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PersonTitleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 