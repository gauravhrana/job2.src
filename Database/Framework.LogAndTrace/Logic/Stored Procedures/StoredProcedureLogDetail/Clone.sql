IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailClone')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailClone'
	DROP  Procedure StoredProcedureLogDetailClone
END
GO

PRINT 'Creating Procedure StoredProcedureLogDetailClone'
GO

/*********************************************************************************************
**		File: 
**		Name: StoredProcedureLogDetailClone
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
**		Change Histor
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.StoredProcedureLogDetailClone
(
		@StoredProcedureLogDetailId		INT			= NULL 				
	,	@StoredProcedureLogId			INT			= NULL 				
	,	@ParameterName					VARCHAR(50)						
	,	@ParameterValue					VARCHAR(50)						
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL				
	,	@SystemEntityType				VARCHAR(50)	= 'StoredProcedureLogDetail'
)
AS
BEGIN	
	
	SELECT	@StoredProcedureLogId		= StoredProcedureLogId
		,	@ParameterName				= ParameterName
		,   @ParameterValue				= ParameterValue			
	FROM	dbo.StoredProcedureLogDetail
	WHERE   StoredProcedureLogDetailId		= @StoredProcedureLogDetailId

	EXEC dbo.StoredProcedureLogDetailInsert 
			@StoredProcedureLogDetailId	=	NULL
		,	@StoredProcedureLogId		= @StoredProcedureLogId
		,	@ParameterName				= @ParameterName
		,   @ParameterValue				= @ParameterValue
		
	

END	
GO
