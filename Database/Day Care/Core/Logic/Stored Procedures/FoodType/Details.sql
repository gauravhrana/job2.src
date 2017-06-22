IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FoodTypeDetails')
BEGIN
	PRINT 'Dropping Procedure FoodTypeDetails'
	DROP  Procedure FoodTypeDetails
END
GO

PRINT 'Creating Procedure FoodTypeDetails'
GO

/******************************************************************************
**		File: 
**		Name: FoodTypeDetails
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

CREATE Procedure dbo.FoodTypeDetails
(
		@FoodTypeId			INT
	,   @AuditId			INT			
    ,   @AuditDate			DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'FoodType'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@FoodTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	FoodTypeId		
		,	ApplicationId
		,	Name					
		,	Description		
		,	SortOrder	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	dbo.FoodType 
	WHERE	FoodTypeId = @FoodTypeId
		
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @FoodTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   