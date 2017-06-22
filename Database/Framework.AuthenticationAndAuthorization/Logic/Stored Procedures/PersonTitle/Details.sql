IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonTitleDetails')
BEGIN
	PRINT 'Dropping Procedure PersonTitleDetails'
	DROP  Procedure PersonTitleDetails
END
GO

PRINT 'Creating Procedure PersonTitleDetails'
GO

/******************************************************************************
**		File: 
**		Name: PersonTitleDetails
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

CREATE Procedure dbo.PersonTitleDetails
(
		@PersonTitleId		INT					
	,	@AuditId			INT					
	,	@AuditDate			DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'PersonTitle'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@PersonTitleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	PersonTitleId		
		,	Name						
		,	Description			
		,	SortOrder
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'	
	FROM	dbo.PersonTitle 
	WHERE	PersonTitleId = @PersonTitleId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PersonTitleId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   