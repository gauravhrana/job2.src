IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DateRangeTitleUpdate')
BEGIN
	PRINT 'Dropping Procedure DateRangeTitleUpdate'
	DROP  Procedure  DateRangeTitleUpdate
END
GO

PRINT 'Creating Procedure DateRangeTitleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: DateRangeTitleUpdate
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.DateRangeTitleUpdate
(
		@DateRangeTitleId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(500)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'DateRangeTitle'
)
AS
BEGIN
	UPDATE	dbo.DateRangeTitle 
	SET		Name					=	@Name				
		,	[Description]			=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	DateRangeTitleId		=	@DateRangeTitleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DateRangeTitle'
		,	@EntityKey				= @DateRangeTitleId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO