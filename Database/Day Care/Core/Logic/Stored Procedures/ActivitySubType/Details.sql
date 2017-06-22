﻿IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivitySubTypeDetails')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeDetails'
	DROP  Procedure ActivitySubTypeDetails
END
GO

PRINT 'Creating Procedure ActivitySubTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: ActivitySubTypeDetails
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

CREATE Procedure dbo.ActivitySubTypeDetails
(
		@ActivitySubTypeID		INT
	,   @AuditId			    INT			
    ,   @AuditDate		        DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50)	= 'ActivitySubType'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ActivitySubTypeID
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	ActivitySubTypeId	
		,	ApplicationId		
		,	ActivityTypeId					
		,	Name	    
		,	Description		
		,	SortOrder	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'						
	FROM	dbo.ActivitySubType 
	WHERE	ActivitySubTypeId = @ActivitySubTypeId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @ActivitySubTypeId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   