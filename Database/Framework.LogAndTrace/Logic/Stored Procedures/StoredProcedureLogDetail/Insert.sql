IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailInsert')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailInsert'
	DROP  Procedure StoredProcedureLogDetailInsert
END
GO

PRINT 'Creating Procedure StoredProcedureLogDetailInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:StoredProcedureLogDetailInsert
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

CREATE Procedure dbo.StoredProcedureLogDetailInsert
(
		@StoredProcedureLogDetailId		INT				OUTPUT		
	,	@StoredProcedureLogId			INT				= NULL 				
	,	@ParameterName					VARCHAR(50)		= 'N/A'				
	,	@ParameterValue					VARCHAR(50)		= 'N/A'				
	,	@SystemEntityType				VARCHAR(50)		= 'StoredProcedureLogDetail'
)
AS
BEGIN
    
	SET @ParameterValue = ISNULL(@ParameterValue, 'N/A')
	
	SET IDENTITY_INSERT dbo.StoredProcedureLogDetail  ON
	INSERT INTO dbo.StoredProcedureLogDetail 
	( 
			StoredProcedureLogDetailId						
		,	StoredProcedureLogId			
		,	ParameterName					
		,	ParameterValue				
			
	)
	VALUES 
	(  
			@StoredProcedureLogDetailId							
		,	@StoredProcedureLogId				
		,	@ParameterName			
		,	@ParameterValue			
	)
	SET IDENTITY_INSERT dbo.StoredProcedureLogDetail  OFF
END	
GO

 