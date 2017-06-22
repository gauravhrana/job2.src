IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CountryUpdate')
BEGIN
	PRINT 'Dropping Procedure CountryUpdate'
	DROP  Procedure  CountryUpdate
END
GO

PRINT 'Creating Procedure CountryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: CountryUpdate
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

CREATE Procedure dbo.CountryUpdate
(
		@CountryId					INT			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT	
	,	@TimeZoneId					INT
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'Country'
)
AS
BEGIN
	UPDATE	dbo.Country 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder	
		,	TimeZoneId			=	@TimeZoneId						
	WHERE	CountryId	=	@CountryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Country'
		,	@EntityKey				= @CountryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO