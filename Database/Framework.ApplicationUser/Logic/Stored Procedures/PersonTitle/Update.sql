IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonTitleUpdate')
BEGIN
	PRINT 'Dropping Procedure PersonTitleUpdate'
	DROP  Procedure  PersonTitleUpdate
END
GO

PRINT 'Creating Procedure PersonTitleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: PersonTitleUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.PersonTitleUpdate
(
		@PersonTitleId		INT		 			
	,	@Name				VARCHAR(50)					
	,	@Description		VARCHAR(50)				
	,	@SortOrder			INT						
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'PersonTitle'	
)
AS
BEGIN 

	UPDATE  dbo.PersonTitle 
	SET		Name			=	@Name				
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder							
	WHERE	PersonTitleId	=	@PersonTitleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @PersonTitleId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		

 GO