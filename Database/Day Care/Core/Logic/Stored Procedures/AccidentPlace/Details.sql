IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentPlaceDetails')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceDetails'
	DROP  Procedure  AccidentPlaceDetails
END
GO

PRINT 'Creating Procedure AccidentPlaceDetails'
GO


/******************************************************************************
**		File: 
**		Name: AccidentPlaceDetails
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

CREATE Procedure dbo.AccidentPlaceDetails
(
		@AccidentPlaceId	INT
	,   @AuditId			INT
	,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'AccidentPlace'

)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@AccidentPlaceId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT


	SELECT	AccidentPlaceId	
		,	ApplicationId
		,	Name					
		,	Description		
		,	SortOrder
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'									
	FROM	dbo.AccidentPlace 
	WHERE	AccidentPlaceId = @AccidentPlaceId	

	--Create Audit History
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @AccidentPlaceId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END		
GO
   